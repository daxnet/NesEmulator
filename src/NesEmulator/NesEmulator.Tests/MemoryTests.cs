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
            var mem = GetMemoryBytes(memory);
            Assert.That(mem[1], Is.EqualTo(0xff));
        }

        [Test]
        public void ReadByteTest()
        {
            SetMemoryByte(memory, 0x2f, 0xff);
            var b = memory.ReadByte(0x2f);
            Assert.That(b, Is.EqualTo(0xff));
        }

        [Test]
        public void ReadWordTest()
        {
            SetMemoryByte(memory, 0x01, 0x12);
            SetMemoryByte(memory, 0x02, 0x34);
            var w = memory.ReadWord(0x01);
            Assert.That(w, Is.EqualTo(0x3412));
        }

        [Test]
        public void WriteWordTest()
        {
            memory.WriteWord(0x2f, 0xfed1);
            var bytes = GetMemoryBytes(memory);
            Assert.That(bytes[0x2f], Is.EqualTo(0xd1));
            Assert.That(bytes[0x30], Is.EqualTo(0xfe));
        }

        private static byte[] GetMemoryBytes(Memory memory)
        {
            return (byte[])(typeof(Memory)
                .GetField("_mem", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(memory) ?? new byte[0]);
        }

        private static void SetMemoryByte(Memory memory, ushort offset, byte value)
        {
            var bytes = GetMemoryBytes(memory);
            bytes[offset] = value;
            typeof(Memory)
                .GetField("_mem", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(memory, bytes);
        }
    }
}
