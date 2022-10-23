using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Implicit, 0x28, 1, 4)]
    internal sealed class PLP : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var flags = cpu.PopByte();
            cpu.StatusFlags.Flags = flags;
        }
    }
}
