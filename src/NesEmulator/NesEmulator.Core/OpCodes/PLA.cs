using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Implicit, 0x68, 1, 4)]
    internal sealed class PLA : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.SetRegister(RegisterNames.A, cpu.PopByte());
        }
    }
}
