using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Implicit, 0xaa, 1, 2)]
    internal sealed class TAX : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.X = cpu.A;
            if (cpu.X == 0)
            {
                cpu.StatusFlags.Z = true;
            }
            if ((cpu.X & 0x80) != 0)
            {
                cpu.StatusFlags.N = true;
            }
        }
    }
}
