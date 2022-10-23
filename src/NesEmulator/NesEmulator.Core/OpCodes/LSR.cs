using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Accumulator, 0x4a, 1, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x46, 2, 5)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x56, 2, 6)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x4e, 3, 6)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x5e, 3, 7)]
    internal sealed class LSR : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            byte result;
            switch (opCodeDefinition.AddressingMode)
            {
                case AddressingMode.Accumulator:
                    result = CalculateLsr(cpu, cpu.A);
                    cpu.SetRegister(RegisterNames.A, result);
                    break;
                default:
                    var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
                    result = CalculateLsr(cpu, memory.ReadByte(address));
                    memory.WriteByte(address, result);
                    cpu.UpdateZeroAndNegativeFlags(result);
                    break;
            }
        }

        private static byte CalculateLsr(Cpu cpu, byte input)
        {
            cpu.StatusFlags.C = Bit.HasSet(input, 0);
            return (byte)(input >> 1);
        }
    }
}
