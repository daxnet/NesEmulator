using NesEmulator.Core.Mappers;
using NesEmulator.Core.OpCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core
{
    public sealed class Emulator : IDisposable
    {

        #region Private Fields

        private readonly Cpu _cpu;
        private readonly StreamWriter? _logStreamWriter;
        private readonly Memory _memory;
        private readonly OpCodeDefinitionAttribute[] _opCodeDefinitions = new OpCodeDefinitionAttribute[0x100];
        private readonly OpCode?[] _opCodes = new OpCode?[0x100];
        private readonly EmulatorOptions _options;
        private readonly List<Mapper> _supportedMappers = new List<Mapper>();
        private Mapper _currentMapper;
        private Cartridge? _installedCartridge;
        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public Emulator() : this(EmulatorOptions.Default) { }

        public Emulator(EmulatorOptions options)
        {
            _currentMapper = new DMAMapper(this);
            _options = options;
            if (_options.EnableLogging && _options.LogOutputStream != null)
            {
                _logStreamWriter = new StreamWriter(_options.LogOutputStream);
            }

            var opCodeList = from p in GetType().Assembly.GetTypes()
                             let opCodeDefAttrs = p.GetCustomAttributes<OpCodeDefinitionAttribute>()
                             where opCodeDefAttrs != null && opCodeDefAttrs.Any()
                             select new { OpCodeImplType = p, OpCodeDefs = opCodeDefAttrs };
            foreach (var item in opCodeList)
            {
                foreach (var opCodeDef in item.OpCodeDefs)
                {
                    if (_opCodes[opCodeDef.OpCode] != null)
                    {
                        throw new Exception("The OpCode has already been loaded.");
                    }
                    _opCodeDefinitions[opCodeDef.OpCode] = opCodeDef;
                    _opCodes[opCodeDef.OpCode] = (OpCode?)Activator.CreateInstance(item.OpCodeImplType);
                }
            }

            var mapperTypes = from p in GetType().Assembly.GetTypes()
                              where p.IsSubclassOf(typeof(Mapper)) && p.IsDefined(typeof(MapperAttribute), false)
                              select p;
            foreach (var mapperType in mapperTypes)
            {
                if (Activator.CreateInstance(mapperType, this) is Mapper mapperInstance)
                {
                    _supportedMappers.Add(mapperInstance);
                }
            }

            _cpu = new Cpu(this, _opCodeDefinitions, _opCodes);
            _memory = new Memory(this);
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<OpCodeExecutionEventArgs>? OpCodeExecuted;

        public event EventHandler<OpCodeExecutionEventArgs>? OpCodeExecuting;

        #endregion Public Events

        #region Public Properties

        public Cpu Cpu => _cpu;

        /// <summary>
        /// Gets the mapper that is currently used by the installed cartridge.
        /// </summary>
        public Mapper CurrentMapper => _currentMapper;
        /// <summary>
        /// Gets the cartridge that has been installed to the emulator.
        /// </summary>
        public Cartridge? InstalledCartridge => _installedCartridge;

        public Memory Memory => _memory;

        #endregion Public Properties

        #region Public Methods

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void Install(Cartridge cartridge)
        {
            _installedCartridge = cartridge;
            var mapper = _supportedMappers.FirstOrDefault(m => m.MapperId == cartridge.MapperType);
            if (mapper == null)
            {
                throw new NotSupportedException($"Mapper {cartridge.MapperType} is not supported.");
            }

            _currentMapper = mapper;
        }

        #endregion Public Methods

        #region Internal Methods

        internal void OnOpCodeExecuted(OpCodeExecutionEventArgs e)
        {
            OpCodeExecuted?.Invoke(this, e);
        }

        internal void OnOpCodeExecuting(OpCodeExecutionEventArgs e)
        {
            if (_options.EnableLogging && _options.LogOutputStream != null)
            {
                _logStreamWriter?.WriteLine(GenerateLogText(e));
            }

            OpCodeExecuting?.Invoke(this, e);
        }

        #endregion Internal Methods

        #region Private Methods

        private static string GenerateLogText(OpCodeExecutionEventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append($"{e.PC - 1,-6:X4}");
            sb.Append($"{e.OpCodeDefinition.OpCode:X2} {string.Join(" ", e.Operand.Select(o => string.Format("{0:X2}", o))),-6}");

            var instructionStr = string.Empty;
            if (e.OpCodeDefinition.AddressingMode == AddressingMode.Relative)
            {
                instructionStr = $"{e.OpCode} ${e.PC + 1 + (sbyte)e.Operand[0]:X4}";
            }
            else
            {
                instructionStr = e.OpCode.Disassemble(e.Operand, e.OpCodeDefinition);
            }

            var (pointer, addr, val) = e.DumpMemoryValue();

            switch (e.OpCodeDefinition.AddressingMode)
            {
                case AddressingMode.ZeroPage:
                    instructionStr = $"{instructionStr} = {val:X2}";
                    break;
                case AddressingMode.ZeroPageX:
                case AddressingMode.ZeroPageY:
                    instructionStr = $"{instructionStr} @ {addr & 0xff:X2} = {val:X2}";
                    break;
                case AddressingMode.Absolute:
                    if (e.OpCodeDefinition.OpCode != 0x4c && e.OpCodeDefinition.OpCode != 0x20)
                    {
                        instructionStr = $"{instructionStr} = {val:X2}";
                    }

                    break;
                case AddressingMode.AbsoluteX:
                case AddressingMode.AbsoluteY:
                    instructionStr = $"{instructionStr} @ {addr:X4} = {val:X2}";
                    break;
                case AddressingMode.Indirect:
                    instructionStr = $"{instructionStr} = {pointer:X4}";
                    break;
                case AddressingMode.IndexedIndirect:
                    instructionStr = $"{instructionStr} @ {pointer & 0xff:X2} = {addr:X4} = {val:X2}";
                    break;
                case AddressingMode.IndirectIndexed:
                    instructionStr = $"{instructionStr} = {pointer:X4} @ {addr:X4} = {val:X2}";
                    break;
            }

            if (e.OpCodeDefinition.Unofficial)
            {
                sb.Append($"*{instructionStr,-32}");
            }
            else
            {
                sb.Append($" {instructionStr,-32}");
            }
            sb.Append($"A:{e.A:X2} X:{e.X:X2} Y:{e.Y:X2} P:{e.P:X2} SP:{e.SP:X2}");
            return sb.ToString();
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _logStreamWriter?.Flush();
                    _logStreamWriter?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Private Methods

    }
}
