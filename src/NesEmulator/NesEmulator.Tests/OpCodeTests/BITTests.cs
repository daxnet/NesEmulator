using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class BITTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void ZeroflagTest()
        {
            TestUtils.SetMemoryByte(emulator.Memory, 0x50, 0b0101_0101);
            var program = new byte[] { 0x24, 0x50 };
            emulator.Cpu.LoadAndRun(program, e =>
            {
                e.Cpu.A = 0b1010_1010;
                e.Cpu.StatusFlags.Z = false;
            });
            Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)true));
        }

        [Test]
        public void NegativeflagTest()
        {
            TestUtils.SetMemoryByte(emulator.Memory, 0x50, 0b1101_0101);
            var program = new byte[] { 0x24, 0x50 };
            emulator.Cpu.LoadAndRun(program, e =>
            {
                e.Cpu.A = 0b1010_1010;
                e.Cpu.StatusFlags.N = false;
            });
            Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)true));
        }

        [Test]
        public void OverflowflagTest()
        {
            TestUtils.SetMemoryByte(emulator.Memory, 0x50, 0b0101_0101);
            var program = new byte[] { 0x24, 0x50 };
            emulator.Cpu.LoadAndRun(program, e =>
            {
                e.Cpu.A = 0b0110_1010;
                e.Cpu.StatusFlags.V = false;
            });
            Assert.That(emulator.Cpu.StatusFlags.V, Is.EqualTo((Bit)true));
        }

        [Test]
        public void AbsoluteAddressingTest()
        {
            TestUtils.SetMemoryByte(emulator.Memory, 0x1234, 0b0101_0101);
            var program = new byte[] { 0x2c, 0x34, 0x12 };
            emulator.Cpu.LoadAndRun(program, e =>
            {
                e.Cpu.A = 0b1010_1010;
                e.Cpu.StatusFlags.Z = false;
            });
            Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)true));
        }
    }
}
