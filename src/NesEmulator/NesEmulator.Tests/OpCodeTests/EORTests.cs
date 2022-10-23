using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class EORTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void ZeroflagTest()
        {
            var program = new byte[] { 0x49, 0b1010_0101 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b1010_0101);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)1));
            });
        }

        [Test]
        public void NegativeflagTest()
        {
            var program = new byte[] { 0x49, 0b1010_0101 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b0010_0101);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0b1000_0000));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)1));
            });
        }
    }
}
