using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class INYTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void ZeroflagTest()
        {
            var program = new byte[] { 0xc8 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.Y = 0xff);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.Y, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)1));
            });
        }

        [Test]
        public void NegativeflagTest()
        {
            var program = new byte[] { 0xc8 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.Y = 0b0111_1111);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.Y, Is.EqualTo(0b1000_0000));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)1));
            });
        }
    }
}
