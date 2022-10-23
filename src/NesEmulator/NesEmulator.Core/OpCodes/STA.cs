using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x85, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x95, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x8d, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x9d, 3, 5)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0x99, 3, 5)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0x81, 2, 6)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0x91, 2, 6)]
    internal sealed class STA : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            memory.WriteByte(address, cpu.A);
        }
    }
}
