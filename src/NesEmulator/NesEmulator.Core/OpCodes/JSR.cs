using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Absolute, 0x20, 3, 6)]
    internal sealed class JSR : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            cpu.PushWord((ushort)(cpu.PC + 2 - 1));
            cpu.PC = address;
        }

        protected override void IncreaseProgramCounter(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition)
        { }
    }
}
