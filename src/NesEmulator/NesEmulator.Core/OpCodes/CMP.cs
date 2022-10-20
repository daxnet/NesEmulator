using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Immediate, 0xc9, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0xc5, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xd5, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xcd, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0xdd, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0xd9, 3, 4)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0xc1, 2, 6)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0xd1, 2, 5)]
    internal sealed class CMP : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var val = memory.ReadByte(address);
            var result = cpu.A - val;
            if (result >= 0)
            {
                cpu.StatusFlags.C = true;
                if (result == 0)
                {
                    cpu.StatusFlags.Z = true;
                }
            }
            else
            {
                cpu.StatusFlags.N = true;
            }
        }
    }
}
