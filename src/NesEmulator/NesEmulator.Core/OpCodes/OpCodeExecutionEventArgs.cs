using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    /// <summary>
    /// Represents the event data that holds the emulator status during the opcode execution.
    /// </summary>
    public sealed class OpCodeExecutionEventArgs : EventArgs
    {
        #region Private Fields

        private readonly ushort _address;
        private readonly Memory _memory;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <c>OpCodeExecutionEventArgs</c> class.
        /// </summary>
        /// <param name="cpu">The instance of the CPU.</param>
        /// <param name="memory">The instance of the memory.</param>
        /// <param name="address">The operand address of the current opcode execution.</param>
        /// <param name="opCodeDefinition">The opcode definition.</param>
        /// <param name="opCode">The opcode instance.</param>
        /// <param name="operand">The raw operand.</param>
        public OpCodeExecutionEventArgs(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition, OpCode opCode, byte[] operand)
        {
            A = cpu.A;
            X = cpu.X;
            Y = cpu.Y;
            P = cpu.StatusFlags.Flags;
            SP = cpu.SP;
            Cycle = cpu.Cycle;
            PC = cpu.PC;
            OpCodeDefinition = opCodeDefinition;
            OpCode = opCode;
            Operand = operand;
            _memory = memory;
            _address = address;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the value of the accumulator.
        /// </summary>
        public byte A { get; init; }

        /// <summary>
        /// Gets the CPU cycle.
        /// </summary>
        public int Cycle { get; init; }

        /// <summary>
        /// Gets the instance of the current opcode.
        /// </summary>
        public OpCode OpCode { get; init; }

        /// <summary>
        /// Gets the instance of the current opcode definition.
        /// </summary>
        public OpCodeDefinitionAttribute OpCodeDefinition { get; init; }

        /// <summary>
        /// Gets the operand of the instruction.
        /// </summary>
        public byte[] Operand { get; init; }

        /// <summary>
        /// Gets the value of the status flags.
        /// </summary>
        public byte P { get; init; }

        /// <summary>
        /// Gets the value of the program counter.
        /// </summary>
        public ushort PC { get; init; }

        /// <summary>
        /// Gets the value of the stack pointer.
        /// </summary>
        public byte SP { get; init; }

        /// <summary>
        /// Gets the value of the X register.
        /// </summary>
        public byte X { get; init; }

        /// <summary>
        /// Gets the value of the Y register.
        /// </summary>
        public byte Y { get; init; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Dumps the memory value of the current execution.
        /// </summary>
        /// <returns>A tuple that holds the information of the memory access.
        /// <list type="bullet">
        /// <item>pointer: The pointer to the memory where the final address is resolved.</item>
        /// <item>address: The resolved address.</item>
        /// <item>result: The memory value at the resolved address.</item>
        /// </list>
        /// </returns>
        public (ushort pointer, ushort address, byte result) DumpMemoryValue()
        {
            if (OpCodeDefinition.AddressingMode == AddressingMode.Relative || Operand.Length == 0)
            {
                return (0, 0, 0);
            }

            ushort operand;
            if (Operand.Length == 1)
            {
                operand = Operand[0];
            }
            else
            {
                operand = (ushort)((Operand[1] << 8) | Operand[0]);
            }

            ushort pointer = 0;
            switch(OpCodeDefinition.AddressingMode)
            {
                case AddressingMode.Immediate:
                    pointer = PC;
                    break;
                case AddressingMode.ZeroPage:
                case AddressingMode.Absolute:
                    pointer = operand;
                    break;
                case AddressingMode.ZeroPageX:
                    pointer = (operand + X).WrapAsByte();
                    break;
                case AddressingMode.ZeroPageY:
                    pointer = (operand + Y).WrapAsByte();
                    break;
                case AddressingMode.AbsoluteX:
                    pointer = (operand + X).WrapAsUShort();
                    break;
                case AddressingMode.AbsoluteY:
                    pointer = (operand +Y).WrapAsUShort();
                    break;
                case AddressingMode.Indirect:
                    var hiAddress = (ushort)((operand & 0xff) == 0xff ? operand - 0xff : operand + 1);
                    pointer = (_memory.ReadByte(hiAddress) << 8 | _memory.ReadByte(operand)).WrapAsUShort();
                    break;
                case AddressingMode.IndexedIndirect:
                    pointer = (operand + X).WrapAsUShort();
                    break;
                case AddressingMode.IndirectIndexed:
                    var lo = _memory.ReadByte(operand);
                    var hi = _memory.ReadByte((operand + 1).WrapAsByte());
                    pointer = (ushort)(hi << 8 | lo);
                    break;
            }

            return (pointer, _address, (byte)(_memory.ReadByte(_address) & 0xff));
        }

        #endregion Public Methods
    }
}
