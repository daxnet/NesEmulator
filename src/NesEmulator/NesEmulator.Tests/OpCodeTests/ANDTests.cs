using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public sealed class ANDTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void ImmediateAndTest()
        {
            var program = new byte[] { 0x29, 0x25 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0xe8);
            Assert.That(emulator.Cpu.A, Is.EqualTo(0x20));
        }

        [Test]
        public void ImmediateAndWithZeroFlag()
        {
            var program = new byte[] { 0x29, 0b0101_0101 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b1010_1010);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)true));
            });
        }

        [Test]
        public void ImmediateAndWithNegativeFlag()
        {
            var program = new byte[] { 0x29, 0b1101_0101 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b1010_1010);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0x80));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)true));
            });
        }
    }
}
