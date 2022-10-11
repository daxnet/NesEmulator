using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Implicit, 0xd8, 1, 2)]
    internal sealed class CLD : OpCode
    {
        protected override void DoExecute(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.StatusFlags.D = 0;
        }
    }
}
