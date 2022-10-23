using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Implicit, 0x40, 1, 6)]
    internal sealed class RTI : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.StatusFlags.Flags = cpu.PopByte();
            cpu.PC = cpu.PopWord();
        }
    }
}
