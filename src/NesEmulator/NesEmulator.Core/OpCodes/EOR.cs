using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Immediate, 0x49, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x45, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x55, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x4d, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x5d, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0x59, 3, 4)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0x41, 2, 6)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0x51, 2, 5)]
    internal sealed class EOR : OpCode
    {
        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var b = memory.ReadByte(address);
            cpu.SetRegister(RegisterNames.A, (byte)(cpu.A ^ b));
        }

        #endregion Protected Methods
    }
}
