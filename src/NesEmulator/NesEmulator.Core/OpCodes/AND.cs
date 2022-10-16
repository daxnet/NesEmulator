using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Immediate, 0x29, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x25, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x35, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x2d, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x3d, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0x39, 3, 4)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0x21, 2, 6)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0x31, 2, 5)]
    internal sealed class AND : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var val = memory.ReadByte(address);
            cpu.SetRegister(RegisterNames.A, (byte)(cpu.A & val));
        }
    }
}
