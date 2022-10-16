using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Immediate, 0xa9, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0xa5, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xb5, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xad, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0xbd, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0xb9, 3, 4)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0xa1, 2, 6)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0xb1, 2, 5)]
    internal sealed class LDA : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var byteVal = memory.ReadByte(address);
            cpu.SetRegister(RegisterNames.A, byteVal);
        }
    }
}
