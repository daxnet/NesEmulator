using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x24, 2, 3)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x2c, 3, 4)]
    internal sealed class BIT : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var val = memory.ReadByte(address);
            var result = (byte)(val & cpu.A);
            if (result == 0)
            {
                cpu.StatusFlags.Z = true;
            }

            cpu.StatusFlags.V = Bit.GetBit(val, 6);
            cpu.StatusFlags.N = Bit.GetBit(val, 7);
        }
    }
}
