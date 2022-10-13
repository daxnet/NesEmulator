using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class StatusFlagsOpCodesTests
    {
        [Test]
        public void CLCTest()
        {
            var program = new byte[] { 0x18 };
            var emulator = new Emulator();
            emulator.Cpu.StatusFlags.C = 1;
            emulator.Cpu.LoadAndRun(program);
            Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)0));
        }

        [Test]
        public void CLDTest()
        {
            var program = new byte[] { 0xd8 };
            var emulator = new Emulator();
            emulator.Cpu.StatusFlags.D = 1;
            emulator.Cpu.LoadAndRun(program);
            Assert.That(emulator.Cpu.StatusFlags.D, Is.EqualTo((Bit)0));
        }

        [Test]
        public void CLITest()
        {
            var program = new byte[] { 0x58 };
            var emulator = new Emulator();
            emulator.Cpu.StatusFlags.I = 1;
            emulator.Cpu.LoadAndRun(program);
            Assert.That(emulator.Cpu.StatusFlags.I, Is.EqualTo((Bit)0));
        }

        [Test]
        public void CLVTest()
        {
            var program = new byte[] { 0xb8 };
            var emulator = new Emulator();
            emulator.Cpu.StatusFlags.V = 1;
            emulator.Cpu.LoadAndRun(program);
            Assert.That(emulator.Cpu.StatusFlags.V, Is.EqualTo((Bit)0));
        }

        [Test]
        public void SECTest()
        {
            var program = new byte[] { 0x38 };
            var emulator = new Emulator();
            emulator.Cpu.StatusFlags.C = 0;
            emulator.Cpu.LoadAndRun(program);
            Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)1));
        }

        [Test]
        public void SEDTest()
        {
            var program = new byte[] { 0xf8 };
            var emulator = new Emulator();
            emulator.Cpu.StatusFlags.D = 0;
            emulator.Cpu.LoadAndRun(program);
            Assert.That(emulator.Cpu.StatusFlags.D, Is.EqualTo((Bit)1));
        }

        [Test]
        public void SEITest()
        {
            var program = new byte[] { 0x78 };
            var emulator = new Emulator();
            emulator.Cpu.StatusFlags.I = 0;
            emulator.Cpu.LoadAndRun(program);
            Assert.That(emulator.Cpu.StatusFlags.I, Is.EqualTo((Bit)1));
        }
    }
}
