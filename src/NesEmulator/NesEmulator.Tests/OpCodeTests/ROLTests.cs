using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class ROLTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void AccumulatorCarryflagTest()
        {
            var program = new byte[] { 0x2a };
            emulator.Cpu.LoadAndRun(program, e =>
            {
                e.Cpu.A = 0b1010_0100;
                e.Cpu.StatusFlags.C = 0;
            });
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0b0100_1000));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo(Bit.BitSet));
            });
        }

        [Test]
        public void AccumulatorZeroflagTest()
        {
            var program = new byte[] { 0x2a };
            emulator.Cpu.LoadAndRun(program, e =>
            {
                e.Cpu.A = 0b1000_0000;
                e.Cpu.StatusFlags.C = 0;
            });
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo(Bit.BitSet));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo(Bit.BitSet));
            });
        }

        [Test]
        public void AccumulatorNegativeflagTest()
        {
            var program = new byte[] { 0x2a };
            emulator.Cpu.LoadAndRun(program, e =>
            {
                e.Cpu.A = 0b0100_0000;
                e.Cpu.StatusFlags.C = 1;
            });
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0b1000_0001));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo(Bit.BitClear));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo(Bit.BitSet));
            });
        }
    }
}
