using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core
{
    public sealed class Memory : NesComponent
    {
        private const int MAX_SIZE = 0x10000;

        private readonly byte[] _mem = new byte[MAX_SIZE];

        public Memory(Emulator emulator) : base(emulator) { }

        public byte ReadByte(ushort pos) => _mem[pos];

        public void WriteByte(ushort pos, byte value) => _mem[pos] = value;

        public ushort ReadWord(ushort pos) => (ushort)(_mem[pos + 1] << 8 | _mem[pos]);

        public void WriteWord(ushort pos, ushort value)
        {
            _mem[pos] = (byte)(value & 0xff);
            _mem[pos + 1] = (byte)(value >> 8);
        }

        public void Reset() => Array.Clear(_mem, 0, _mem.Length);
    }
}
