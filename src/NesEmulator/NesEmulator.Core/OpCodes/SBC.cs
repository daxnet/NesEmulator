using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Immediate, 0xe9, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0xe5, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xf5, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xed, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0xfd, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0xf9, 3, 4)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0xe1, 2, 6)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0xf1, 2, 5)]
    internal sealed class SBC : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var val = memory.ReadByte(address);
            cpu.AddToRegisterA((byte)~val);
        }
    }
}
