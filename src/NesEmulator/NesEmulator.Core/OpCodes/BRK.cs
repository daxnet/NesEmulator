using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Implicit, 0x00, 1, 7)]
    internal sealed class BRK : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.PushWord(cpu.PC);
            cpu.PushByte(cpu.StatusFlags.Flags);
            cpu.PC = memory.ReadWord(0xfffe);
            cpu.StatusFlags.I = true;
        }
    }
}
