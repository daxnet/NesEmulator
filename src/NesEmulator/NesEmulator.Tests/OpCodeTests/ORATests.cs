using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class ORATests
    {
        private readonly Emulator emulator = new Emulator();

        [Test]
        public void ImmediateAddressingNegativeflagTest()
        {
            var program = new byte[] { 0x9, 0b0101_0101 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b1010_1010);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0xff));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo(Bit.BitSet));
            });
        }

        [Test]
        public void ImmediateAddressingZeroflagTest()
        {
            var program = new byte[] { 0x9, 0 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo(Bit.BitSet));
            });
        }
    }
}
