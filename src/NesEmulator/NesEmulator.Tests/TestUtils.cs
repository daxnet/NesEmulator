using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests
{
    internal class TestUtils
    {
        public static byte[] GetMemoryBytes(Memory memory)
        {
            return (byte[])(typeof(Memory)
                .GetField("_mem", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(memory) ?? new byte[0]);
        }

        public static void SetMemoryByte(Memory memory, ushort offset, byte value)
        {
            var bytes = GetMemoryBytes(memory);
            bytes[offset] = value;
            typeof(Memory)
                .GetField("_mem", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(memory, bytes);
        }

        public static byte GetMemoryByte(Memory memory, ushort offset)
        {
            var bytes = GetMemoryBytes(memory);
            return bytes[offset];
        }
    }
}
