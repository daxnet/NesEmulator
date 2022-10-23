using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class LSRTests
    {
        private readonly Emulator emulator = new Emulator();

        [Test]
        public void AcumulatorCarryflagTest()
        {
            var program = new byte[] { 0x4a };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b0101_0101);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0b0010_1010));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)1));
            });
        }

        [Test]
        public void ZeroPageZeroflagTest()
        {
            TestUtils.SetMemoryByte(emulator.Memory, 0x03, 0b0000_0001);
            var program = new byte[] { 0x46, 0x03 };
            emulator.Cpu.LoadAndRun(program);
            var b = TestUtils.GetMemoryByte(emulator.Memory, 0x03);
            Assert.Multiple(() =>
            {
                Assert.That(b, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)1));
            });
        }
    }
}
