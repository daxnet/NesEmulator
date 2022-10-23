using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class SBCTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void AcumulatorZeroflagTest()
        {
            var program = new byte[] { 0xe9, 0x04 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x05);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo(Bit.BitSet));
            });

        }

        [Test]
        public void AcumulatorNoZeroflagTest()
        {
            var program = new byte[] { 0xe9, 0x03 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x05);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(1));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo(Bit.BitClear));
            });

        }

        [Test]
        public void AcumulatorNegativeTest()
        {
            var program = new byte[] { 0xe9, 0x09 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x05);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0xfb));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo(Bit.BitSet));
            });

        }
    }
}
