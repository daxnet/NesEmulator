using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.ZeroPage, 0xc6, 2, 5)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xd6, 2, 6)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xce, 3, 6)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0xde, 3, 7)]
    internal sealed class DEC : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var m = memory.ReadByte(address);
            m--;
            memory.WriteByte(address, m);
            cpu.UpdateZeroAndNegativeFlags(m);
        }
    }
}
