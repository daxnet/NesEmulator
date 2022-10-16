using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class ADCTests
    {
        private readonly Emulator emulator = new Emulator();

        [Test]
        public void ImmediateAddressingSimpleTest()
        {
            var program = new byte[] { 0x69, 0x03 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x05);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0x08));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)0));
            });
        }

        [Test]
        public void ImmediateAddressingCarryflagTest()
        {
            var program = new byte[] { 0x69, 0xfe };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x05);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0x03));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)1));
            });
        }

        [Test]
        public void ImmediateAddressingNoOverflowTest()
        {
            var program = new byte[] { 0x69, 0xfe };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x05);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0x03));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)1));
                Assert.That(emulator.Cpu.StatusFlags.V, Is.EqualTo((Bit)0));
            });
        }

        [Test]
        public void ImmediateAddressingHasOverflowTest()
        {
            var program = new byte[] { 0x69, 0x70 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x70);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0xe0));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)0));
                Assert.That(emulator.Cpu.StatusFlags.V, Is.EqualTo((Bit)1));
            });
        }

        [Test]
        public void ImmediateAddressingHasCarryAndOverflowTest()
        {
            // LDA #$81
            // ADC #$80
            var program = new byte[] { 0x69, 0x80 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x81);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0x01));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)1));
                Assert.That(emulator.Cpu.StatusFlags.V, Is.EqualTo((Bit)1));
            });
        }

        [Test]
        public void ZeroPageAddressingTest()
        {
            // LDA #$03
            // STA $05
            // LDA #$05
            // ADC $05
            TestUtils.SetMemoryByte(emulator.Memory, 0x05, 0x03);
            var program = new byte[] { 0x65, 0x05 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x05);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0x08));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)0));
            });
        }

        [Test]
        public void ZeroPageXAddressingTest()
        {
            // LDA #$03
            // STA $05
            // LDA #$05
            // LDX #$03
            // ADC $02,X
            TestUtils.SetMemoryByte(emulator.Memory, 0x05, 0x03);
            var program = new byte[] { 0x75, 0x02 };
            emulator.Cpu.LoadAndRun(program, e =>
            {
                e.Cpu.X = 0x03;
                e.Cpu.A = 0x05;
            });
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0x08));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)0));
            });
        }
    }
}
