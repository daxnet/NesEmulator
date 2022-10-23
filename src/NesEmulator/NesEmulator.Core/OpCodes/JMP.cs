using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Absolute, 0x4c, 3, 3)]
    [OpCodeDefinition(AddressingMode.Indirect, 0x6c, 3, 5)]
    internal sealed class JMP : OpCode
    {
        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
            => cpu.PC = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);

        protected override void IncreaseProgramCounter(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition)
        { }
        #endregion Protected Methods
    }
}
