using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class JSRTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void AbsoluteAddressingTest()
        {
            var program = new byte[] { 0x20, 0x3f, 0x0d };
            emulator.Cpu.LoadAndRun(program);
            var b1 = TestUtils.GetMemoryByte(emulator.Memory, Cpu.StackPageOffset + Cpu.StackResetValue);
            var b2 = TestUtils.GetMemoryByte(emulator.Memory, Cpu.StackPageOffset + Cpu.StackResetValue - 1);
            var w = (b1 << 8) | b2;
            Assert.Multiple(() =>
            {
                Assert.That(w, Is.EqualTo(0x8002));
                Assert.That(emulator.Cpu.PC, Is.EqualTo(0x0d3f));
                Assert.That(emulator.Cpu.SP, Is.EqualTo(Cpu.StackResetValue - 2));
            });
        }
    }
}
