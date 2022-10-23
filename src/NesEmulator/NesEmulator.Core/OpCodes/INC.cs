using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.ZeroPage, 0xe6, 2, 5)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xf6, 2, 6)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xee, 3, 6)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0xfe, 3, 7)]
    internal sealed class INC : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var b = memory.ReadByte(address);
            b++;
            memory.WriteByte(address, b);
            cpu.UpdateZeroAndNegativeFlags(b);
        }
    }
}
