using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class BPLTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void JumpTest()
        {
            var program = new byte[] { 0x10, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.N = 0);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8004));
        }

        [Test]
        public void NoJumpTest()
        {
            var program = new byte[] { 0x10, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.N = 1);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8002));
        }
    }
}
