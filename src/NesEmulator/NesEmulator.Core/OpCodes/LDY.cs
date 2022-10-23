using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Immediate, 0xa0, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0xa4, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xb4, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xac, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0xbc, 3, 4)]
    internal sealed class LDY : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var byteVal = memory.ReadByte(address);
            cpu.SetRegister(RegisterNames.Y, byteVal);
        }
    }
}
