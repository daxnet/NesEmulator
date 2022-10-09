using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    public abstract class OpCode
    {
        public abstract void Execute(byte opcode, Cpu cpu);

        public override string ToString() => this.GetType().Name;
    }
}
