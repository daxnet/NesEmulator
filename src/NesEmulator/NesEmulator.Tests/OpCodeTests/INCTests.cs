using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class INCTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void ZeroflagTest()
        {
            TestUtils.SetMemoryByte(emulator.Memory, 0x10, 0xff);
            var program = new byte[] { 0xe6, 0x10 };
            emulator.Cpu.LoadAndRun(program);
            var b = TestUtils.GetMemoryByte(emulator.Memory, 0x10);
            Assert.That(b, Is.EqualTo(0));
            Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)1));
        }

        [Test]
        public void NegativeflagTest()
        {
            TestUtils.SetMemoryByte(emulator.Memory, 0x10, 0b0111_1111);
            var program = new byte[] { 0xe6, 0x10 };
            emulator.Cpu.LoadAndRun(program);
            var b = TestUtils.GetMemoryByte(emulator.Memory, 0x10);
            Assert.That(b, Is.EqualTo(0b1000_0000));
            Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)1));
        }
    }
}
