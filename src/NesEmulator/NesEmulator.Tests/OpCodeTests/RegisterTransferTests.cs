using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class RegisterTransferTests
    {
        private readonly Emulator emulator = new Emulator();

        [Test]
        public void TAXTest()
        {
            var program = new byte[] { 0xaa };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x9a);
            Assert.That(emulator.Cpu.X, Is.EqualTo(0x9a));
            Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)true));
        }

        [Test]
        public void TAXZeroTest()
        {
            var program = new byte[] { 0xaa };
            emulator.Cpu.LoadAndRun(program, e => { e.Cpu.X = 0x9a; e.Cpu.A = 0; });
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.X, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)false));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)true));
            });
        }

        [Test]
        public void TAYTest()
        {
            var program = new byte[] { 0xa8 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x9a);
            Assert.That(emulator.Cpu.Y, Is.EqualTo(0x9a));
            Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)true));
        }


        [Test]
        public void TAYZeroTest()
        {
            var program = new byte[] { 0xa8 };
            emulator.Cpu.LoadAndRun(program, e => { e.Cpu.Y = 0x9a; e.Cpu.A = 0; });
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.Y, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)false));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)true));
            });
        }

        [Test]
        public void TSXTest()
        {
            var program = new byte[] { 0xba };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.SP = 0x9a);
            Assert.That(emulator.Cpu.X, Is.EqualTo(0x9a));
            Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)true));
        }


        [Test]
        public void TSXZeroTest()
        {
            var program = new byte[] { 0xba };
            emulator.Cpu.LoadAndRun(program, e => { e.Cpu.X = 0x9a; e.Cpu.SP = 0; });
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.X, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)false));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)true));
            });
        }

        [Test]
        public void TXATest()
        {
            var program = new byte[] { 0x8a };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.X = 0x9a);
            Assert.That(emulator.Cpu.A, Is.EqualTo(0x9a));
            Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)true));
        }


        [Test]
        public void TXAZeroTest()
        {
            var program = new byte[] { 0x8a };
            emulator.Cpu.LoadAndRun(program, e => { e.Cpu.A = 0x9a; e.Cpu.X = 0; });
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)false));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)true));
            });
        }

        [Test]
        public void TXSTest()
        {
            var program = new byte[] { 0x9a };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.X = 0xa1);
            Assert.That(emulator.Cpu.SP, Is.EqualTo(0xa1));
        }

        [Test]
        public void TYATest()
        {
            var program = new byte[] { 0x98 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.Y = 0x9a);
            Assert.That(emulator.Cpu.A, Is.EqualTo(0x9a));
            Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)true));
        }


        [Test]
        public void TYAZeroTest()
        {
            var program = new byte[] { 0x98 };
            emulator.Cpu.LoadAndRun(program, e => { e.Cpu.A = 0x9a; e.Cpu.Y = 0; });
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)false));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)true));
            });
        }

        [Test]
        public void STATest()
        {
            var program = new byte[] { 0x85, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => { e.Cpu.A = 0x99; });
            var b = TestUtils.GetMemoryByte(emulator.Memory, 0x02);
            Assert.That(b, Is.EqualTo(0x99));
        }

        [Test]
        public void STXTest()
        {
            var program = new byte[] { 0x86, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => { e.Cpu.X = 0x99; });
            var b = TestUtils.GetMemoryByte(emulator.Memory, 0x02);
            Assert.That(b, Is.EqualTo(0x99));
        }

        [Test]
        public void STYTest()
        {
            var program = new byte[] { 0x84, 0x02 };
            emulator.Cpu.LoadAndRun(program, e => { e.Cpu.Y = 0x99; });
            var b = TestUtils.GetMemoryByte(emulator.Memory, 0x02);
            Assert.That(b, Is.EqualTo(0x99));
        }
    }
}
