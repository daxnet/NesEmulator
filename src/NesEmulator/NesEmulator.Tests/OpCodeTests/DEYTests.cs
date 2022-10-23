using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class DEYTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void ZeroflagTest()
        {
            var program = new byte[] { 0x88 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.Y = 0x01);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.Y, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)1));
            });
        }

        [Test]
        public void NegativeflagTest()
        {
            var program = new byte[] { 0x88 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.Y = 0x00);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.Y, Is.EqualTo(0xff));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)1));
            });
        }
    }
}
