using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class ASLTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void AccumulatorAddressingTest()
        {
            var program = new byte[] { 0x0a };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b0000_0001);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0b0000_0010));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)0));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)0));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)0));
            });
        }

        [Test]
        public void AccumulatorAddressingWithCarryflagTest()
        {
            var program = new byte[] { 0x0a };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b1010_0000);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0b0100_0000));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)1));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)0));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)0));
            });
        }

        [Test]
        public void AccumulatorAddressingWithCarryAndNegativeflagTest()
        {
            var program = new byte[] { 0x0a };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b1110_0000);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0b1100_0000));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)1));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)0));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)1));
            });
        }

        [Test]
        public void AccumulatorAddressingWithCarryAndZeroflagTest()
        {
            var program = new byte[] { 0x0a };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b1000_0000);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)1));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)1));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)0));
            });
        }

        [Test]
        public void ZeroPageAddressingTest()
        {
            TestUtils.SetMemoryByte(emulator.Memory, 0xfe, 0b0000_0001);
            var program = new byte[] { 0x06, 0xfe };
            emulator.Cpu.LoadAndRun(program);
            Assert.Multiple(() =>
            {
                var b = emulator.Memory.ReadByte(0xfe);
                Assert.That(b, Is.EqualTo(0b0000_0010));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)0));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)0));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)0));
            });
        }
    }
}
