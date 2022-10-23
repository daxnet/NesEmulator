using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class LDXTests
    {
        private readonly Emulator emulator = new Emulator();

        [Test]
        public void ImmediateAddressingZeroflagTest()
        {
            var program = new byte[] { 0xa2, 0x00 };
            emulator.Cpu.LoadAndRun(program, e =>
            {
                e.Cpu.X = 1;
            });
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.X, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)true));
            });
        }

        [Test]
        public void ImmediateAddressingNegativeflagTest()
        {
            var program = new byte[] { 0xa2, 0x80 };
            emulator.Cpu.LoadAndRun(program);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.X, Is.EqualTo(0x80));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)true));
            });
        }
    }
}
