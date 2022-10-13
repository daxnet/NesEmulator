using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class LDATests
    {
        private readonly Emulator emulator = new Emulator();

        [Test]
        public void ImmediateAddressingNegativeValueTest()
        {
            // LDA #$F2
            var program = new byte[] { 0xa9, 0xf2 };
            emulator.Cpu.LoadAndRun(program);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0xf2));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)1));
            });
        }

        [Test]
        public void ImmediateAddressingZeroValueTest()
        {
            // LDA #$00
            var program = new byte[] { 0xa9, 0x00 };
            emulator.Cpu.LoadAndRun(program);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)1));
            });
        }

        [Test]
        public void ZeroPageAddressingTest()
        {
            // LDA #$90
            // STA $50
            // LDA $50
            TestUtils.SetMemoryByte(emulator.Memory, 0x50, 0x90);
            var program = new byte[] { 0xa5, 0x50 };
            emulator.Cpu.LoadAndRun(program);
            Assert.That(emulator.Cpu.A, Is.EqualTo(0x90));
        }

        [Test]
        public void ZeroPageXAddressingTest()
        {
            // LDA #$23
            // STA $45
            // LDX #$02
            // LDA $43,X
            TestUtils.SetMemoryByte(emulator.Memory, 0x45, 0x23);
            var program = new byte[] { 0xb5, 0x43 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.X = 0x02);
            Assert.That(emulator.Cpu.A, Is.EqualTo(0x23));
        }

        [Test]
        public void AbsoluteAddressingTest()
        {
            TestUtils.SetMemoryByte(emulator.Memory, 0x8110, 0x77);
            var program = new byte[] { 0xad, 0x10, 0x81 };
            emulator.Cpu.LoadAndRun(program);
            Assert.That(emulator.Cpu.A, Is.EqualTo(0x77));
        }
    }
}
