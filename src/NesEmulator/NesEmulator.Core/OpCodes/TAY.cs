using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Implicit, 0xa8, 1, 2)]
    internal sealed class TAY : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.Y = cpu.A;
            if (cpu.Y == 0)
            {
                cpu.StatusFlags.Z = true;
            }
            if ((cpu.Y & 0x80) != 0)
            {
                cpu.StatusFlags.N = true;
            }
        }
    }
}
