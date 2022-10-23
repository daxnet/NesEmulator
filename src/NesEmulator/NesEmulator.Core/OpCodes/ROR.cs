using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Accumulator, 0x6a, 1, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x66, 2, 5)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x76, 2, 6)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x6e, 3, 6)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x7e, 3, 7)]
    internal sealed class ROR : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            byte result;
            switch (opCodeDefinition.AddressingMode)
            {
                case AddressingMode.Accumulator:
                    result = CalculateRor(cpu, cpu.A);
                    cpu.SetRegister(RegisterNames.A, result);
                    break;
                default:
                    var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
                    result = CalculateRor(cpu, memory.ReadByte(address));
                    memory.WriteByte(address, result);
                    cpu.UpdateZeroAndNegativeFlags(result);
                    break;
            }
        }

        private static byte CalculateRor(Cpu cpu, byte src)
        {
            var oldCarryFlag = cpu.StatusFlags.C;
            cpu.StatusFlags.C = Bit.HasSet(src, 0);
            return (byte)((src >> 1) | (oldCarryFlag.Value << 7));
        }
    }
}
