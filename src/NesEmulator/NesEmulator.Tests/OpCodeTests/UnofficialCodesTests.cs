using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class UnofficialCodesTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void ANCNegativeTest()
        {
            var program = new byte[] { 0xb, 0b1011_0110 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b1000_1001);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0x80));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo(Bit.BitSet));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo(Bit.BitSet));
            });
        }

        [Test]
        public void ANCZeroTest()
        {
            var program = new byte[] { 0x2b, 0b1011_0110 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b0000_1001);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo(Bit.BitClear));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo(Bit.BitClear));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo(Bit.BitSet));
            });
        }

        [Test]
        public void SAXNegativeTest()
        {
            var program = new byte[] { 0x87, 0x10 };
            emulator.Cpu.LoadAndRun(program, e =>
            {
                e.Cpu.A = 0b1011_0110;
                e.Cpu.X = 0b1010_0100;
            });
            var b = TestUtils.GetMemoryByte(emulator.Memory, 0x10);
            Assert.That(b, Is.EqualTo(0b1010_0100));
        }

        [Test]
        public void SAXZeroTest()
        {
            var program = new byte[] { 0x87, 0x10 };
            emulator.Cpu.LoadAndRun(program, e =>
            {
                e.Cpu.A = 0b1011_0010;
                e.Cpu.X = 0b0000_0100;
            });
            var b = TestUtils.GetMemoryByte(emulator.Memory, 0x10);
            Assert.That(b, Is.EqualTo(0));
        }

        [Test]
        public void ARRBothBit5And6SetTest()
        {
            var program = new byte[] { 0x6b, 0b1111_1101 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b1101_1101);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0b1110_1110));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo(Bit.BitSet));
                Assert.That(emulator.Cpu.StatusFlags.V, Is.EqualTo(Bit.BitClear));
            });
        }

        [Test]
        public void ARRBothBit5And6ClearTest()
        {
            var program = new byte[] { 0x6b, 0b1011_1101 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b0101_1101);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0b1000_1110));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo(Bit.BitClear));
                Assert.That(emulator.Cpu.StatusFlags.V, Is.EqualTo(Bit.BitClear));
            });
        }

        [Test]
        public void ARROnlyBit5SetTest()
        {
            var program = new byte[] { 0x6b, 0b1111_1101 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b0101_1101);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0b1010_1110));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo(Bit.BitClear));
                Assert.That(emulator.Cpu.StatusFlags.V, Is.EqualTo(Bit.BitSet));
            });
        }

        [Test]
        public void ARROnlyBit6SetTest()
        {
            var program = new byte[] { 0x6b, 0b1111_1101 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b1001_1101);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0b1100_1110));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo(Bit.BitSet));
                Assert.That(emulator.Cpu.StatusFlags.V, Is.EqualTo(Bit.BitSet));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo(Bit.BitSet));
            });
        }

        [Test]
        public void ARRZeroflagSetTest()
        {
            var program = new byte[] { 0x6b, 0b0101_0101 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b1010_1010);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0));
                Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo(Bit.BitClear));
                Assert.That(emulator.Cpu.StatusFlags.V, Is.EqualTo(Bit.BitClear));
                Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo(Bit.BitSet));
            });
        }

        [Test]
        public void ASRTest()
        {
            var program = new byte[] { 0x4b, 0b1101_0101 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b1110_1011);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0b0110_0000));
            });
        }

        [Test]
        public void ATXTest()
        {
            var program = new byte[] { 0xab, 0b1101_0101 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0b1110_1011);
            Assert.Multiple(() =>
            {
                Assert.That(emulator.Cpu.A, Is.EqualTo(0b1100_0001));
                Assert.That(emulator.Cpu.X, Is.EqualTo(0b1100_0001));
                Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo(Bit.BitSet));
            });
        }

        [Test]
        public void AXATest()
        {
            var program = new byte[] { 0x9f, 0x05, 0x02 };
            emulator.Cpu.LoadAndRun(program, e =>
            {
                e.Cpu.X = 0b1010_0110;
                e.Cpu.A = 0b1010_1111;
                e.Cpu.Y = 0x03;
            });
            var b = TestUtils.GetMemoryByte(emulator.Memory, 0x0208);
            Assert.That(b, Is.EqualTo(0b110));
        }

        [Test]
        public void AXSTest()
        {
            var program = new byte[] { 0xcb, 0x2 };
            emulator.Cpu.LoadAndRun(program, e =>
            {
                e.Cpu.X = 0b0101_0110;
                e.Cpu.A = 0b0111_1100;
            });
            Assert.That(emulator.Cpu.X, Is.EqualTo(0x52));
        }
    }
}
