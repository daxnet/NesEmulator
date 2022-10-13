using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Implicit, 0x98, 1, 2)]
    internal sealed class TYA : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.A = cpu.Y;
            if (cpu.A == 0)
            {
                cpu.StatusFlags.Z = true;
            }
            if ((cpu.A & 0x80) != 0)
            {
                cpu.StatusFlags.N = true;
            }
        }
    }
}
