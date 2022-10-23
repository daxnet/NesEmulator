using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Immediate, 0x9, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x5, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x15, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xd, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x1d, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0x19, 3, 4)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0x1, 2, 6)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0x11, 2, 5)]
    internal sealed class ORA : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var byteVal = memory.ReadByte(address);
            cpu.SetRegister(RegisterNames.A, (byte)(cpu.A | byteVal));
        }
    }
}
