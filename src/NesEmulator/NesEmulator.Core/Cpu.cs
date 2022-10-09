using NesEmulator.Core.OpCodes;
using System.Reflection;

namespace NesEmulator.Core
{
    public sealed class Cpu : NesComponent
    {

        #region Private Fields

        private const byte StackResetValue = 0xfd;
        private const byte StatusFlagResetValue = 0x34;

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

        public void LoadAndRun(byte[] program)
        {
            Load(program);
            Reset();
            Run();
        }

        public void Reset()
        {
            _a = 0;
            _x = 0;
            _y = 0;
            _sp = StackResetValue;
            _statusFlags.Flags = StatusFlagResetValue;
            _pc = Emulator.Memory.ReadWord(0xfffc);
        }

        #endregion Public Methods

        #region Internal Methods

        internal OpCodeDefinitionAttribute GetOpCodeDefinition(byte opcode) => _opCodeDefinitions[opcode];

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
                opCodeImpl?.Execute(inst, this);
                inst = Emulator.Memory.ReadByte(_pc);
            }
        }

        #endregion Private Methods

        internal ushort GetOperandAddress(AddressingMode mode)
        {
            return mode switch
            {
                AddressingMode.Immediate => _pc,
                AddressingMode.ZeroPage => Emulator.Memory.ReadByte(_pc),
                AddressingMode.ZeroPageX => (Emulator.Memory.ReadByte(_pc) + _x).WrapAsByte(),
                AddressingMode.ZeroPageY => (Emulator.Memory.ReadByte(_pc) + _y).WrapAsByte(),
                AddressingMode.Absolute => Emulator.Memory.ReadWord(_pc),
                AddressingMode.AbsoluteX => (Emulator.Memory.ReadWord(_pc) + _x).WrapAsUShort(),
                AddressingMode.AbsoluteY => (Emulator.Memory.ReadWord(_pc) + _y).WrapAsUShort(),
                _ => throw new Exception()
            };
        }


    }
}