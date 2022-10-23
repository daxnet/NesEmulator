using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class StackTests
    {
        private readonly Emulator emulator = new Emulator();

        [Test]
        public void PHATest()
        {
            var program = new byte[] { 0x48 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x12);
            var b = TestUtils.GetMemoryByte(emulator.Memory, Cpu.StackPageOffset + Cpu.StackResetValue);
            Assert.That(b, Is.EqualTo(0x12));
        }

        [Test]
        public void PHPTest()
        {
            var program = new byte[] { 0x8 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.Flags = 0b1011_0101);
            var b = TestUtils.GetMemoryByte(emulator.Memory, Cpu.StackPageOffset + Cpu.StackResetValue);
            Assert.That(b, Is.EqualTo(0b1011_0101));
        }

        [Test]
        public void PLATest()
        {
            var program = new byte[] { 0x68 };
            TestUtils.SetMemoryByte(emulator.Memory, Cpu.StackPageOffset + Cpu.StackResetValue, 0x12);
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.SP = Cpu.StackResetValue - 1);
            Assert.That(emulator.Cpu.A, Is.EqualTo(0x12));
        }

        [Test]
        public void PLPTest()
        {
            TestUtils.SetMemoryByte(emulator.Memory, Cpu.StackPageOffset + Cpu.StackResetValue, 0b1010_1010);
            var program = new byte[] { 0x28 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.SP = Cpu.StackResetValue - 1);
            Assert.That(emulator.Cpu.StatusFlags.Flags, Is.EqualTo(0b1010_1010));
        }
    }
}
