using NesEmulator.Core.OpCodes;
using System.Reflection;

namespace NesEmulator.Core
{
    public sealed class Cpu : NesComponent
    {

        #region Private Fields

        private const byte StackResetValue = 0xfd;
        private const byte StatusFlagResetValue = 0x34; // 0b0011_0100

        private readonly OpCodeDefinitionAttribute[] _opCodeDefinitions = new OpCodeDefinitionAttribute[0x100];
        private readonly OpCode?[] _opCodes = new OpCode?[0x100];
        private byte _a;
        private ushort _pc;
        private byte _sp;
        private StatusFlags _statusFlags;
        private byte _x;
        private byte _y;

        #endregion Private Fields

        #region Public Constructors

        public Cpu(Emulator emulator) : base(emulator)
        {
            var opCodeList = from p in GetType().Assembly.GetTypes()
                             let opCodeDefAttrs = p.GetCustomAttributes<OpCodeDefinitionAttribute>()
                             where opCodeDefAttrs != null && opCodeDefAttrs.Any()
                             select new { OpCodeImplType = p, OpCodeDefs = opCodeDefAttrs };
            foreach(var item in opCodeList)
            {
                foreach (var opCodeDef in item.OpCodeDefs)
                {
                    _opCodeDefinitions[opCodeDef.OpCode] = opCodeDef;
                    _opCodes[opCodeDef.OpCode] = (OpCode?)Activator.CreateInstance(item.OpCodeImplType);
                }
            }

            Reset();
        }

        #endregion Public Constructors

        #region Public Properties

        public byte A
        {
            get => _a; set => _a = value;
        }

        public ushort PC
        {
            get => _pc; set => _pc = value;
        }

        public byte SP
        {
            get => _sp; set => _sp = value;
        }
        public ref StatusFlags StatusFlags
        {
            get => ref _statusFlags;
        }

        public byte X
        {
            get => _x; set => _x = value;
        }

        public byte Y
        {
            get => _y; set => _y = value;
        }

        #endregion Public Properties

        #region Public Methods

        public void LoadAndRun(byte[] program, Action<Emulator>? stateSetter = null)
        {
            Load(program);
            Reset();
            stateSetter?.Invoke(Emulator);
            Run();
        }

        public void Reset()
        {
            _a = 0;
            _x = 0;
            _y = 0;
            _sp = StackResetValue;
            _statusFlags.Flags = StatusFlagResetValue;
            _pc = Emulator.Memory?.ReadWord(0xfffc) ?? 0;
        }

        #endregion Public Methods

        #region Internal Methods

        internal OpCodeDefinitionAttribute GetOpCodeDefinition(byte opcode) => _opCodeDefinitions[opcode];

        /// <summary>
        /// Computes the operand address based on the given addressing mode.
        /// </summary>
        /// <param name="mode">The memory addressing mode.</param>
        /// <returns>The operand address.</returns>
        /// <exception cref="NotSupportedException">Throws when the addressing 
        /// mode is not supported</exception>
        internal ushort GetOperandAddress(AddressingMode mode)
        {
            ushort GetIndexedIndirectAddress()
            {
                var ptr = (Emulator.Memory.ReadByte(_pc) + _x).WrapAsByte();
                var lo = Emulator.Memory.ReadByte(ptr);
                var hi = Emulator.Memory.ReadByte((ptr + 1).WrapAsUShort());
                return (ushort)(hi << 8 | lo);
            }

            ushort GetIndirectIndexedAddress()
            {
                var baseAddress = Emulator.Memory.ReadByte(_pc);
                var lo = Emulator.Memory.ReadByte(baseAddress);
                var hi = Emulator.Memory.ReadByte((baseAddress + 1).WrapAsUShort());
                var rebaseAddress = (ushort)(hi << 8 | lo);
                return (rebaseAddress + _y).WrapAsUShort();
            }


            return mode switch
            {
                AddressingMode.Immediate => _pc,
                AddressingMode.ZeroPage => Emulator.Memory.ReadByte(_pc),
                AddressingMode.ZeroPageX => (Emulator.Memory.ReadByte(_pc) + _x).WrapAsByte(),
                AddressingMode.ZeroPageY => (Emulator.Memory.ReadByte(_pc) + _y).WrapAsByte(),
                AddressingMode.Absolute => Emulator.Memory.ReadWord(_pc),
                AddressingMode.AbsoluteX => (Emulator.Memory.ReadWord(_pc) + _x).WrapAsUShort(),
                AddressingMode.AbsoluteY => (Emulator.Memory.ReadWord(_pc) + _y).WrapAsUShort(),
                AddressingMode.IndexedIndirect => GetIndexedIndirectAddress(),
                AddressingMode.IndirectIndexed => GetIndirectIndexedAddress(),
                _ => throw new NotSupportedException($"{mode} is not supported.")
            };
        }

        internal void SetRegister(RegisterNames register, byte val, bool updateZandNFlags = true)
        {
            switch(register)
            {
                case RegisterNames.A:
                    A = val;
                    break;
                case RegisterNames.X:
                    X = val;
                    break;
                case RegisterNames.Y:
                    Y = val;
                    break;
            }

            if (updateZandNFlags)
            {
                UpdateZeroAndNegativeFlags(val);
            }
        }

        internal void UpdateZeroAndNegativeFlags(byte val)
        {
            if (val == 0)
            {
                StatusFlags.Z = true;
            }
            if ((val & 0x80) != 0)
            {
                StatusFlags.N = true;
            }
        }

        /// <summary>
        /// Adds a byte value to the accumulator and sets the
        /// status flags properly.
        /// </summary>
        /// <param name="val">The value to be added to the accumulator.</param>
        internal void AddToRegisterA(byte val)
        {
            // firstly add the given value to the value in the
            // accumulator and take the carry flag into account.
            var sum = A + val + StatusFlags.C;

            // The carry flag is set if the sum value is greater
            // than a byte's max value.
            StatusFlags.C = sum > byte.MaxValue;

            // Sets the overflow flag. When the value being added
            // to the accumulator has the different sign than the result,
            // AND the value in the accumulator also has a different sign
            // than the result, the overflow flag should be set.
            var result = (byte)sum;
            StatusFlags.V = ((val ^ result) & (A ^ result) & 0x80) != 0;

            // Sets the value to the register A.
            SetRegister(RegisterNames.A, result);
        }

        #endregion Internal Methods

        #region Private Methods

        /// <summary>
        /// Loads the program to be executed.
        /// </summary>
        /// <param name="program">The byte array that holds the program to be executed.</param>
        private void Load(byte[] program)
        {
            if (program == null || program.Length == 0)
                throw new ArgumentNullException(nameof(program));
            Emulator.Memory.CopyFrom(program, 0x8000);
            Emulator.Memory.WriteWord(0xfffc, 0x8000);
        }
        private void Run()
        {
            var inst = Emulator.Memory.ReadByte(_pc);
            while (inst != 0)
            {
                _pc++;
                var opCodeImpl = _opCodes[inst];
                opCodeImpl?.Execute(inst, this, Emulator.Memory);
                inst = Emulator.Memory.ReadByte(_pc);
            }
        }

        #endregion Private Methods

    }
}