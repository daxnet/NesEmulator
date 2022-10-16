using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Immediate, 0x69, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x65, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x75, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x6d, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x7d, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0x79, 3, 4)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0x61, 2, 6)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0x71, 2, 5)]
    internal sealed class ADC : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var val = memory.ReadByte(address);
            cpu.AddToRegisterA(val);
        }
    }
}
