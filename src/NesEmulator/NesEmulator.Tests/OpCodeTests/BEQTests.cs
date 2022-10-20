using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class BEQTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void JumpTest()
        {
            var program = new byte[] { 0xf0, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.Z = 1);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8004));
        }

        [Test]
        public void NoJumpTest()
        {
            var program = new byte[] { 0xf0, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.Z = 0);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8002));
        }
    }
}
