using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x86, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageY, 0x96, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x8e, 3, 4)]
    internal sealed class STX : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            memory.WriteByte(address, cpu.X);
        }
    }
}
