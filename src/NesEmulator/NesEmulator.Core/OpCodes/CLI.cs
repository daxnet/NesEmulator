using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Implicit, 0x58, 1, 2)]
    internal sealed class CLI : OpCode
    {
        public override void Execute(byte opcode, Cpu cpu)
        {
            cpu.StatusFlags.I = 0;
        }
    }
}
