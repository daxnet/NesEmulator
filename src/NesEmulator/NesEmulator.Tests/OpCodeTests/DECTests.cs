using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class DECTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void ZeroPageAddressingZeroflagTest()
        {
            TestUtils.SetMemoryByte(emulator.Memory, 0x90, 0x01);
            var program = new byte[] { 0xc6, 0x90 };
            emulator.Cpu.LoadAndRun(program);
            var b = TestUtils.GetMemoryByte(emulator.Memory, 0x90);
            Assert.Multiple(() =>
            {
                Assert.That(b, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)true));
            });
        }

        [Test]
        public void ZeroPageAddressingNegativeflagTest()
        {
            TestUtils.SetMemoryByte(emulator.Memory, 0x90, 0x00);
            var program = new byte[] { 0xc6, 0x90 };
            emulator.Cpu.LoadAndRun(program);
            var b = TestUtils.GetMemoryByte(emulator.Memory, 0x90);
            Assert.Multiple(() =>
            {
                Assert.That(b, Is.EqualTo(0xff));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)true));
            });
        }

        [Test]
        public void ZeroPageXAddressingZeroflagTest()
        {
            TestUtils.SetMemoryByte(emulator.Memory, 0x42, 0x01);
            var program = new byte[] { 0xd6, 0x40 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.X = 0x02);
            var b = TestUtils.GetMemoryByte(emulator.Memory, 0x42);
            Assert.Multiple(() =>
            {
                Assert.That(b, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)true));
            });
        }

        [Test]
        public void AbsoluteAddressingZeroflagTest()
        {
            TestUtils.SetMemoryByte(emulator.Memory, 0x0234, 0x01);
            var program = new byte[] { 0xce, 0x34, 0x02 };
            emulator.Cpu.LoadAndRun(program);
            var b = TestUtils.GetMemoryByte(emulator.Memory, 0x1234);
            Assert.Multiple(() =>
            {
                Assert.That(b, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)true));
            });
        }

        [Test]
        public void AbsoluteXAddressingZeroflagTest()
        {
            TestUtils.SetMemoryByte(emulator.Memory, 0x0234, 0x01);
            var program = new byte[] { 0xde, 0x32, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.X = 0x02);
            var b = TestUtils.GetMemoryByte(emulator.Memory, 0x1234);
            Assert.Multiple(() =>
            {
                Assert.That(b, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)true));
            });
        }
    }
}
