using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Immediate, 0xa2, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0xa6, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageY, 0xb6, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xae, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0xbe, 3, 4)]
    internal sealed class LDX : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var byteVal = memory.ReadByte(address);
            cpu.SetRegister(RegisterNames.X, byteVal);
        }
    }
}
