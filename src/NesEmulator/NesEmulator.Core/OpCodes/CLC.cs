﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Implicit, 0x18, 1, 2)]
    internal sealed class CLC : OpCode
    {
        public override void Execute(byte opcode, Cpu cpu)
        {
            cpu.StatusFlags.C = false;
        }
    }
}
