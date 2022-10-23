using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class JMPTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void AbsoluteAddressingTest()
        {
            var program = new byte[] { 0x4c, 0x2d, 0x3f };
            emulator.Cpu.LoadAndRun(program);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x3f2d));
        }

        [Test]
        public void IndirectAddressingTest()
        {
            TestUtils.SetMemoryByte(emulator.Memory, 0x2d3f, 0x12);
            TestUtils.SetMemoryByte(emulator.Memory, 0x2d40, 0x34);
            var program = new byte[] { 0x6c, 0x3f, 0x2d };
            emulator.Cpu.LoadAndRun(program);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x3412));
        }

        [Test]
        public void IndirectAddressingCrosspageTest()
        {
            TestUtils.SetMemoryByte(emulator.Memory, 0x2d00, 0x11);
            TestUtils.SetMemoryByte(emulator.Memory, 0x2dff, 0x12);
            TestUtils.SetMemoryByte(emulator.Memory, 0x2e00, 0x34);
            var program = new byte[] { 0x6c, 0xff, 0x2d };
            emulator.Cpu.LoadAndRun(program);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x1112));
        }
    }
}
