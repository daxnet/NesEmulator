using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Immediate, 0xc0, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0xc4, 2, 3)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xcc, 3, 4)]
    internal sealed class CPY : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var val = memory.ReadByte(address);
            var result = cpu.Y - val;
            if (result >= 0)
            {
                cpu.StatusFlags.C = true;
                if (result == 0)
                {
                    cpu.StatusFlags.Z = true;
                }
            }
            else
            {
                cpu.StatusFlags.N = true;
            }
        }
    }
}
