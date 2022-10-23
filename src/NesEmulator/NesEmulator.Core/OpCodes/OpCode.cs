﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    public abstract class OpCode
    {
        public void Execute(byte opcode, Cpu cpu, Memory memory)
        {
            var opCodeDefinition = cpu.GetOpCodeDefinition(opcode);
            DoExecute(cpu, memory, opCodeDefinition);
            IncreaseProgramCounter(cpu, opCodeDefinition);
        }

        protected abstract void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition);

        protected virtual void IncreaseProgramCounter(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition) => cpu.PC += (ushort)(opCodeDefinition.Bytes - 1);

        internal string Disassemble(byte opcode, byte[] operand, Cpu cpu)
        {
            var opCodeDefinition = cpu.GetOpCodeDefinition(opcode);
            var sb = new StringBuilder();
            sb.Append(ToString());
            if (opCodeDefinition.AddressingMode != AddressingMode.Implicit &&
                opCodeDefinition.AddressingMode != AddressingMode.Accumulator)
            {
                sb.Append(' ');
                var op = operand.Length == 1 ? operand[0] : (operand[1] << 8 | operand[0]);
                switch (opCodeDefinition.AddressingMode)
                {
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
                        sb.Append($"(${op:X4},X)");
                        break;
                    case AddressingMode.IndirectIndexed:
                        sb.Append($"(${op:X4}),Y");
                        break;
                }
            }

            return sb.ToString();
        }

        public override string ToString() => this.GetType().Name;
    }
}
