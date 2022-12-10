// ============================================================================
//       __ __   __
//  |\ ||_ (_   |_  _    | _ |_ _  _
//  | \||____)  |__||||_||(_||_(_)|
//
// Written by Sunny Chen (daxnet), 2022
// MIT License
// ============================================================================

using System.Diagnostics;
using System.Text;

namespace NesEmulator.Core.OpCodes
{
    public abstract class OpCode
    {

        #region Public Methods

        public string Disassemble(byte[] operand, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var sb = new StringBuilder();
            sb.Append(ToString());
            if (opCodeDefinition.AddressingMode != AddressingMode.Implicit)
            {
                sb.Append(' ');
                int op = 0;
                if (operand.Length > 0)
                {
                    op = operand.Length == 1 ? operand[0] : (operand[1] << 8 | operand[0]);
                }
                switch (opCodeDefinition.AddressingMode)
                {
                    case AddressingMode.Accumulator:
                        sb.Append('A');
                        break;

                    case AddressingMode.Immediate:
                        sb.Append($"#${op:X2}");
                        break;

                    case AddressingMode.Relative:
                    case AddressingMode.ZeroPage:
                        sb.Append($"${op:X2}");
                        break;

                    case AddressingMode.ZeroPageX:
                        sb.Append($"${op:X2},X");
                        break;

                    case AddressingMode.ZeroPageY:
                        sb.Append($"${op:X2},Y");
                        break;

                    case AddressingMode.Absolute:
                        sb.Append($"${op:X4}");
                        break;

                    case AddressingMode.AbsoluteX:
                        sb.Append($"${op:X4},X");
                        break;

                    case AddressingMode.AbsoluteY:
                        sb.Append($"${op:X4},Y");
                        break;

                    case AddressingMode.Indirect:
                        sb.Append($"(${op:X4})");
                        break;

                    case AddressingMode.IndexedIndirect:
                        if ((op & 0xff) == op)
                        {
                            sb.Append($"(${op:X2},X)");
                        }
                        else
                        {
                            sb.Append($"(${op:X4},X)");
                        }
                        break;

                    case AddressingMode.IndirectIndexed:
                        if ((op & 0xff) == op)
                        {
                            sb.Append($"(${op:X2}),Y");
                        }
                        else
                        {
                            sb.Append($"(${op:X4}),Y");
                        }
                        break;
                }
            }

            return sb.ToString();
        }

        public void Execute(byte opcode, Cpu cpu, Memory memory, Action<OpCodeExecutionEventArgs>? executingCallback = null, Action<OpCodeExecutionEventArgs>? executedCallback = null)
        {
            var opCodeDefinition = cpu.GetOpCodeDefinition(opcode);
            var pageCrossed = false;
            var parameterBytes = new byte[opCodeDefinition.Bytes - 1];
            for (var idx = 0; idx < opCodeDefinition.Bytes - 1; idx++)
            {
                parameterBytes[idx] = memory.ReadByte((ushort)(cpu.PC + idx));
            }

            // Get operand address, in the meanwhile, get whether the page was crossed.
            var operandAddress =
                opCodeDefinition.AddressingMode == AddressingMode.Implicit ||
                opCodeDefinition.AddressingMode == AddressingMode.Accumulator ||
                opCodeDefinition.AddressingMode == AddressingMode.Relative
                ?
                ushort.MinValue
                :
                cpu.GetCurrentOperandAddress(opCodeDefinition.AddressingMode, out pageCrossed);

            executingCallback?.Invoke(new OpCodeExecutionEventArgs(cpu, memory, operandAddress, opCodeDefinition, this, parameterBytes));

            // Execute the instruction with the specified operand address.
            DoExecute(cpu, memory, operandAddress, opCodeDefinition);

            executedCallback?.Invoke(new OpCodeExecutionEventArgs(cpu, memory, operandAddress, opCodeDefinition, this, parameterBytes));

            // Adds cycles to CPU.
            cpu.Cycle += opCodeDefinition.Cycles;

            // If page cross check is on and page crossed, add additional cycle.
            if (opCodeDefinition.PageCrossCheck && pageCrossed)
            {
                cpu.Cycle++;
            }

            // Increases program counter.
            IncreaseProgramCounter(cpu, opCodeDefinition);
        }

        public override string ToString() => this.GetType().Name;

        #endregion Public Methods

        #region Internal Methods

        /// <summary>
        /// Retrieves the byte code representation of the current running instruction.
        /// </summary>
        /// <param name="opcode">The hex value of the opcode.</param>
        /// <param name="operand">The operand of the instruction.</param>
        /// <returns>A string that represents the byte code of the current instruction.</returns>
        internal static string GetByteCode(byte opcode, byte[] operand)
        {
            var bytes = new byte[operand.Length + 1];
            bytes[0] = opcode;
            if (operand.Length > 0)
            {
                Array.Copy(operand, 0, bytes, 1, operand.Length);
            }
            return $"{BitConverter.ToString(bytes).Replace("-", string.Empty)}";
        }

        #endregion Internal Methods

        #region Protected Methods

        protected abstract void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition);

        protected virtual void IncreaseProgramCounter(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition) => cpu.PC += (ushort)(opCodeDefinition.Bytes - 1);

        #endregion Protected Methods

    }
}