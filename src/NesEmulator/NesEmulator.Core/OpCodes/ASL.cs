using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Accumulator, 0x0a, 1, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x06, 2, 5)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x16, 2, 6)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x0e, 3, 6)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x1e, 3, 7)]
    internal sealed class ASL : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            byte result;
            switch (opCodeDefinition.AddressingMode)
            {
                case AddressingMode.Accumulator:
                    result = CalculateAsl(cpu, cpu.A);
                    cpu.SetRegister(RegisterNames.A, result);
                    break;
                default:
                    var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
                    result = CalculateAsl(cpu, memory.ReadByte(address));
                    memory.WriteByte(address, result);
                    cpu.UpdateZeroAndNegativeFlags(result);
                    break;
            }
        }

        private static byte CalculateAsl(Cpu cpu, byte input)
        {
            cpu.StatusFlags.C = input >> 7 == 1;
            return (byte)(input << 1);
        }
    }
}
