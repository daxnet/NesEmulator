using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class BranchTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void BCCJumpTest()
        {
            var program = new byte[] { 0x90, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.C = 0);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8004));
        }

        [Test]
        public void BCCNoJumpTest()
        {
            var program = new byte[] { 0x90, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.C = 1);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8002));
        }

        [Test]
        public void BCSJumpTest()
        {
            var program = new byte[] { 0xb0, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.C = 1);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8004));
        }

        [Test]
        public void BCSNoJumpTest()
        {
            var program = new byte[] { 0xb0, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.C = 0);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8002));
        }

        [Test]
        public void BEQJumpTest()
        {
            var program = new byte[] { 0xf0, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.Z = 1);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8004));
        }

        [Test]
        public void BEQNoJumpTest()
        {
            var program = new byte[] { 0xf0, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.Z = 0);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8002));
        }

        [Test]
        public void BMIJumpTest()
        {
            var program = new byte[] { 0x30, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.N = 1);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8004));
        }

        [Test]
        public void BMINoJumpTest()
        {
            var program = new byte[] { 0x30, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.N = 0);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8002));
        }

        [Test]
        public void BNEJumpTest()
        {
            var program = new byte[] { 0xd0, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.Z = 0);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8004));
        }

        [Test]
        public void BNENoJumpTest()
        {
            var program = new byte[] { 0xd0, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.Z = 1);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8002));
        }

        [Test]
        public void BPLJumpTest()
        {
            var program = new byte[] { 0x10, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.N = 0);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8004));
        }

        [Test]
        public void BPLNoJumpTest()
        {
            var program = new byte[] { 0x10, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.N = 1);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8002));
        }

        [Test]
        public void BVCJumpTest()
        {
            var program = new byte[] { 0x50, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.V = 0);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8004));
        }

        [Test]
        public void BVCNoJumpTest()
        {
            var program = new byte[] { 0x50, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.V = 1);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8002));
        }

        [Test]
        public void BVSJumpTest()
        {
            var program = new byte[] { 0x70, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.V = 1);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8004));
        }

        [Test]
        public void BVSNoJumpTest()
        {
            var program = new byte[] { 0x70, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.StatusFlags.V = 0);
            Assert.That(emulator.Cpu.PC, Is.EqualTo(0x8002));
        }
    }
}
