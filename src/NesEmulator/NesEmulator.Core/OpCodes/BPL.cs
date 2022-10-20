using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Relative, 0x10, 2, 2)]
    internal sealed class BPL : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            if (!cpu.StatusFlags.N)
            {
                cpu.Branch();
            }
            else
            {
                base.IncreaseProgramCounter(cpu, opCodeDefinition);
            }
        }

        protected override void IncreaseProgramCounter(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition)
        { }
    }
}
