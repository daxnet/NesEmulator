using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class CPXTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void ZeroflagTest()
        {
            var program = new byte[] { 0xe0, 0x50 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.X = 0x50);
            Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)1));
        }

        [Test]
        public void ZeroflagClearTest()
        {
            var program = new byte[] { 0xe0, 0x50 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.X = 0x51);
            Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)0));
        }

        [Test]
        public void CarryflagTest()
        {
            var program = new byte[] { 0xe0, 0x50 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.X = 0x50);
            Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)1));
        }

        [Test]
        public void Carryflag2Test()
        {
            var program = new byte[] { 0xe0, 0x50 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.X = 0x51);
            Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)1));
        }

        [Test]
        public void CarryflagClearTest()
        {
            var program = new byte[] { 0xe0, 0x50 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.X = 0x49);
            Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)0));
        }

        [Test]
        public void NegativeflagTest()
        {
            var program = new byte[] { 0xe0, 0x50 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.X = 0x49);
            Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)1));
        }

        [Test]
        public void NegativeflagClearTest()
        {
            var program = new byte[] { 0xe0, 0x50 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.X = 0x51);
            Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)0));
        }

        [Test]
        public void ZeroPageAddressingTest()
        {
            // LDA #$50
            // STA $10
            // LDX #$51
            // CPX $10
            TestUtils.SetMemoryByte(emulator.Memory, 0x10, 0x50);
            var program = new byte[] { 0xe4, 0x10 };
            emulator.Cpu.LoadAndRun(program, e =>
            {
                e.Cpu.X = 0x50;
            });
            Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)1));
        }
    }
}
