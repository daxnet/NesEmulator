using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests
{
    public class MemoryTests
    {
        private readonly Memory memory;

        public MemoryTests()
        {
            memory = new Memory(new Emulator());
        }

        [SetUp]
        public void Setup()
        {
            memory.Reset();
        }

        [Test]
        public void WriteByteTest()
        {
            memory.WriteByte(1, 0xff);
            var mem = TestUtils.GetMemoryBytes(memory);
            Assert.That(mem[1], Is.EqualTo(0xff));
        }

        [Test]
        public void ReadByteTest()
        {
            TestUtils.SetMemoryByte(memory, 0x2f, 0xff);
            var b = memory.ReadByte(0x2f);
            Assert.That(b, Is.EqualTo(0xff));
        }

        [Test]
        public void ReadWordTest()
        {
            TestUtils.SetMemoryByte(memory, 0x01, 0x12);
            TestUtils.SetMemoryByte(memory, 0x02, 0x34);
            var w = memory.ReadWord(0x01);
            Assert.That(w, Is.EqualTo(0x3412));
        }

        [Test]
        public void WriteWordTest()
        {
            memory.WriteWord(0x2f, 0xfed1);
            var bytes = TestUtils.GetMemoryBytes(memory);
            Assert.That(bytes[0x2f], Is.EqualTo(0xd1));
            Assert.That(bytes[0x30], Is.EqualTo(0xfe));
        }

        [Test]
        public void CopyBytes1Test()
        {
            var bytes = new byte[] { 0xf1, 0xf2, 0xf3 };
            memory.CopyFrom(bytes);
            var memoryBytes = TestUtils.GetMemoryBytes(memory);
            Assert.Multiple(() =>
            {
                Assert.That(memoryBytes[0], Is.EqualTo(0xf1));
                Assert.That(memoryBytes[1], Is.EqualTo(0xf2));
                Assert.That(memoryBytes[2], Is.EqualTo(0xf3));
                Assert.That(memoryBytes[3], Is.EqualTo(0));
            });
        }

        [Test]
        public void CopyBytes2Test()
        {
            var bytes = new byte[] { 0xf1, 0xf2, 0xf3 };
            memory.CopyFrom(bytes, 0x8000);
            var memoryBytes = TestUtils.GetMemoryBytes(memory);
            Assert.Multiple(() =>
            {
                Assert.That(memoryBytes[0x8000], Is.EqualTo(0xf1));
                Assert.That(memoryBytes[0x8001], Is.EqualTo(0xf2));
                Assert.That(memoryBytes[0x8002], Is.EqualTo(0xf3));
                Assert.That(memoryBytes[0x8003], Is.EqualTo(0));
            });
        }
    }
}
