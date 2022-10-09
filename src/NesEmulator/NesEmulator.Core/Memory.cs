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

        public byte ReadByte(ushort offset) => _mem[offset];

        public void WriteByte(ushort offset, byte value) => _mem[offset] = value;

        public ushort ReadWord(ushort offset) => (ushort)(_mem[offset + 1] << 8 | _mem[offset]);

        public void WriteWord(ushort offset, ushort value)
        {
            _mem[offset] = (byte)(value & 0xff);
            _mem[offset + 1] = (byte)(value >> 8);
        }

        public void CopyFrom(byte[] bytes) => Array.Copy(bytes, _mem, bytes.Length);

        public void CopyFrom(byte[] bytes, int destIndex)
            => Array.Copy(bytes, 0, _mem, destIndex, bytes.Length);

        public void Reset() => Array.Clear(_mem, 0, _mem.Length);
    }
}
