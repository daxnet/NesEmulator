using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x84, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x94, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x8c, 3, 4)]
    internal sealed class STY : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            memory.WriteByte(address, cpu.Y);
        }
    }
}
