using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests
{
    public class CpuTests
    {
        [Test]
        public void SetStatusFlagCTest()
        {
            var emulator = new Emulator();
            emulator.Cpu.StatusFlags.C = 1;
            Assert.That(emulator.Cpu.StatusFlags.Flags, Is.EqualTo(1));
        }
    }
}
