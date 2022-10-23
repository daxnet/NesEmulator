using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Implicit, 0xe8, 1, 2)]
    internal sealed class INX : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition) 
            => cpu.SetRegister(RegisterNames.X, (byte)(cpu.X + 1));
    }
}
