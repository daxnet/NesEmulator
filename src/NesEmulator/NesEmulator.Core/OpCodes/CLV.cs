using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Implicit, 0xb8, 1, 2)]
    internal sealed class CLV : OpCode
    {
        protected override void DoExecute(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.StatusFlags.V = 0;
        }
    }
}
