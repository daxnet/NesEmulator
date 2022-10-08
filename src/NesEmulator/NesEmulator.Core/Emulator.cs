using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core
{
    public sealed class Emulator
    {
        private readonly Cpu _cpu;
        private readonly Memory _memory;

        public Emulator()
        {
            _cpu = new Cpu(this);
            _memory = new Memory(this);
        }
    }
}
