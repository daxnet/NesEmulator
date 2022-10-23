using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class RTSTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void RTSTest()
        {
            var program = new byte[] 
            {
                0x20, 0x10, 0x80, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 
                0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0xea, 0xea, 0x60
            };

            emulator.Cpu.LoadAndRun(program);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8003));
        }
    }
}
