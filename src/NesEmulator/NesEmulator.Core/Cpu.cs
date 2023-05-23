using NesEmulator.Core.OpCodes;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace NesEmulator.Core
{
    public sealed class Cpu : NesComponent
    {

        #region Public Fields

        public const ushort StackPageOffset = 0x100;
        public const byte StackResetValue = 0xfd;
        public const byte StatusFlagResetValue = 0x24;

        #endregion Public Fields

        // 0b0010_0100

        #region Private Fields

        private readonly OpCodeDefinitionAttribute[] _opCodeDefinitions = new OpCodeDefinitionAttribute[0x100];
        private readonly OpCode?[] _opCodes = new OpCode?[0x100];
        private byte _a;
        private int _cycle = 0;
        private ushort _pc;
        private byte _sp;
        private StatusFlags _statusFlags;
        private byte _x;
        private byte _y;

        #endregion Private Fields

        #region Public Constructors

        public Cpu(Emulator emulator, OpCodeDefinitionAttribute[] opCodeDefinitions, OpCode?[] opCodes) : base(emulator)
        {
            _opCodeDefinitions = opCodeDefinitions;
            _opCodes = opCodes;
            Reset();
        }

        #endregion Public Constructors

        #region Public Properties

        public byte A
        {
            get => _a; set => _a = value;
        }

        public int Cycle
        {
            get => _cycle; set => _cycle = value;
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

        public string Disassemble(byte[] program)
        {
            var sb = new StringBuilder();
            var indexer = 0;
            var offset = 0x8000;
            while (indexer < program.Length)
            {
                var opcode = program[indexer];
                indexer++;
                var opCodeImpl = _opCodes[opcode];
                var opCodeDefinition = _opCodeDefinitions[opcode];
                var operandLength = opCodeDefinition.Bytes - 1;
                var operand = new byte[operandLength];
                if (opCodeImpl != null)
                {
                    if (indexer + operandLength > program.Length)
                    {
                        sb.AppendLine($"${offset,-6:X} {OpCode.GetByteCode(opcode, operand),-8} .byte ${opcode:X}");
                        break;
                    }

                    Array.Copy(program, indexer, operand, 0, operandLength);
                    sb.AppendLine($"${offset,-6:X} {OpCode.GetByteCode(opcode, operand),-8} {opCodeImpl.Disassemble(operand, opCodeDefinition)}");
                }

                indexer += operandLength;
                offset += operandLength + 1;
            }

            return sb.ToString();
        }

        public void LoadAndRun(byte[] program, Action<Emulator>? stateSetter = null, ushort address = 0x8000)
        {
            Load(program, address);
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

        public void Run()
        {
            var inst = Emulator.Memory.ReadByte(_pc);
            while (inst != 0)
            {
                _pc++;
                var opCodeImpl = _opCodes[inst];
                opCodeImpl?.Execute(inst, this, Emulator.Memory, Emulator.OnOpCodeExecuting, Emulator.OnOpCodeExecuted);
                inst = Emulator.Memory.ReadByte(_pc);
            }
        }

        #endregion Public Methods

        #region Internal Methods

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

        internal void Branch()
        {
            // +1 if branch succeeds
            _cycle++;

            var brancingPC = (_pc + (sbyte)Emulator.Memory.ReadByte(_pc) + 1).WrapAsUShort();
            if (PageCrossed(_pc, brancingPC))
            {
                // +1 again if page crossed.
                _cycle++;
            }

            _pc = brancingPC;
        }
        /// <summary>
        /// Computes the operand address based on the given addressing mode.
        /// </summary>
        /// <param name="mode">The memory addressing mode.</param>
        /// <returns>The operand address.</returns>
        /// <exception cref="NotSupportedException">Throws when the addressing 
        /// mode is not supported</exception>
        internal ushort GetCurrentOperandAddress(AddressingMode mode, out bool pageCrossed) => ResolveAddress(mode, _pc, out pageCrossed);

        internal OpCodeDefinitionAttribute GetOpCodeDefinition(byte opcode) => _opCodeDefinitions[opcode];


        internal bool PageCrossed(ushort a, ushort b) => (a & 0xff) != (b & 0xff);

        internal byte PopByte()
        {
            _sp++;
            //return Emulator.Memory.ReadByte((ushort)(StackPageOffset + _sp));
            return Emulator.Memory.ReadByte((ushort)(0x100 | _sp));
        }

        internal ushort PopWord()
        {
            var lo = PopByte();
            var hi = PopByte();
            return (ushort)(hi << 8 | lo);
        }

        internal void PushByte(byte val)
        {
            // Emulator.Memory.WriteByte((ushort)(StackPageOffset + _sp), val);
            Emulator.Memory.WriteByte((ushort)(0x100 | _sp), val);
            _sp--;
        }

        internal void PushWord(ushort val)
        {
            PushByte((byte)(val >> 8)); // hi
            PushByte((byte)(val & 0xff)); // lo
        }

        internal ushort ResolveAddress(AddressingMode mode, ushort address, out bool pageCrossed)
        {
            pageCrossed = false;
            ushort result;
            byte hi, lo;
            switch (mode)
            {
                case AddressingMode.Immediate:
                    result = address;
                    break;
                case AddressingMode.ZeroPage:
                    result = Emulator.Memory.ReadByte(address);
                    break;
                case AddressingMode.ZeroPageX:
                    result = (Emulator.Memory.ReadByte(address) + _x).WrapAsByte();
                    break;
                case AddressingMode.ZeroPageY:
                    result = (Emulator.Memory.ReadByte(address) + _y).WrapAsByte();
                    break;
                case AddressingMode.Absolute:
                    result = Emulator.Memory.ReadWord(address);
                    break;
                case AddressingMode.AbsoluteX:
                    result = (Emulator.Memory.ReadWord(address) + _x).WrapAsUShort();
                    pageCrossed = PageCrossed((ushort)(address - _x), _x);
                    break;
                case AddressingMode.AbsoluteY:
                    result = (Emulator.Memory.ReadWord(address) + _y).WrapAsUShort();
                    pageCrossed = PageCrossed((ushort)(address - _y), _y);
                    break;
                case AddressingMode.Indirect:
                    var pointer = Emulator.Memory.ReadWord(address);
                    var hiAddress = (ushort)((pointer & 0xff) == 0xff ? pointer - 0xff : pointer + 1);
                    result = (Emulator.Memory.ReadByte(hiAddress) << 8 | Emulator.Memory.ReadByte(pointer)).WrapAsUShort();
                    break;
                case AddressingMode.IndexedIndirect:
                    pointer = (Emulator.Memory.ReadByte(address) + _x).WrapAsByte();
                    lo = Emulator.Memory.ReadByte(pointer);
                    hi = Emulator.Memory.ReadByte((pointer + 1).WrapAsByte());
                    result = (ushort)(hi << 8 | lo);
                    break;
                case AddressingMode.IndirectIndexed:
                    var baseAddress = Emulator.Memory.ReadByte(address);
                    lo = Emulator.Memory.ReadByte(baseAddress);
                    hi = Emulator.Memory.ReadByte((baseAddress + 1).WrapAsByte());
                    pointer = (ushort)(hi << 8 | lo);
                    result = (pointer + _y).WrapAsUShort();
                    pageCrossed = PageCrossed((ushort)(address - _y), address);
                    break;
                default:
                    throw new NotSupportedException($"{mode} is not supported.");
            }

            return result;
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
            StatusFlags.Z = val == 0;
            StatusFlags.N = ((val >> 7) & 1) == 1;
        }

        #endregion Internal Methods

        #region Private Methods

        /// <summary>
        /// Loads the program to be executed.
        /// </summary>
        /// <param name="program">The byte array that holds the program to be executed.</param>
        /// <param name="address">The address in the emulator memory from where the program is loaded into.</param>
        private void Load(byte[] program, ushort address = 0x8000)
        {
            if (program == null || program.Length == 0)
                throw new ArgumentNullException(nameof(program));
            Emulator.Memory.CopyFrom(program, address);
            Emulator.Memory.WriteWord(0xfffc, address);
        }

        #endregion Private Methods
    }
}