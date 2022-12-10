using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core
{
    // ----------------------------------------------------------------------------------------------------------|
    // |  $0000-$07FF  |  $0800  |  2KB internal RAM                                                             |
    // |---------------+---------+-------------------------------------------------------------------------------|
    // |  $0800-$0FFF  |  $0800  |                                                                               |
    // |---------------+---------|                                                                               |
    // |  $1000-$17FF  |  $0800  |  Mirrors of $0000-$07FF                                                       |
    // |---------------+---------|                                                                               |
    // |  $1800-$1FFF  |  $0800  |                                                                               |
    // |---------------+---------+-------------------------------------------------------------------------------|
    // |  $2000-$2007  |  $0008  |  NES PPU registers                                                            |
    // |---------------+---------+-------------------------------------------------------------------------------|
    // |  $2008-$3FFF  |  $1FF8  |  Mirrors of $2000-2007 (repeats every 8 bytes)                                |
    // |---------------+---------+-------------------------------------------------------------------------------|
    // |  $4000-$4017  |  $0018  |  NES APU and I/O registers                                                    |
    // |---------------+---------+-------------------------------------------------------------------------------|
    // |  $4018-$401F  |  $0008  |  APU and I/O functionality that is normally disabled. See CPU Test Mode.      |
    // |---------------+---------+-------------------------------------------------------------------------------|
    // |  $4020-$FFFF  |  $BFE0  |  Cartridge space: PRG ROM, PRG RAM, and mapper registers.                     |
    // |---------------------------------------------------------------------------------------------------------|

    /// <summary>
    /// Represents the memory.
    /// </summary>
    public sealed class Memory : NesComponent
    {
        private const int MAX_SIZE = 0x10000;

        private readonly byte[] _mem = new byte[MAX_SIZE];

        public Memory(Emulator emulator) : base(emulator) { }

        public byte ReadByte(ushort offset)
        {
            if (offset <= 0x1fff)
            {
                var addr = (ushort)(offset % 0x800);
                return _mem[addr];
            }
            else if (offset <= 0x3fff)
            {
                // TODO: Implement Read Ppu Registers
                return 0;
            }
            else if (offset <= 0x4017)
            {
                // TODO: Implement Read Apu and IO Registers
                return 0;
            }
            else if (offset <= 0x401f)
            {
                return 0;
            }
            else if (offset >= 0x4020)
            {
                // TODO: Implement mapper
                return _mem[offset];
            }
            else
                throw new InvalidOperationException($"Invalid CPU memory address reading at {offset:X}");
        }

        public void WriteByte(ushort offset, byte value)
        {
            if (offset <= 0x1fff)
            {
                var addr = (ushort)(offset % 0x800);
                _mem[addr] = value;
            }
            else if (offset <= 0x3fff)
            {
                // TODO: Implement Write Ppu Registers
            }
            else if (offset <= 0x4017)
            {
                // TODO: Implement Write Apu and IO Registers
            }
            else if (offset <= 0x401f)
            {
                // Do nothing
            }
            else if (offset >= 0x4020)
            {
                // TODO: Implement mapper
                _mem[offset] = value;
            }
            else
                throw new InvalidOperationException($"Invalid CPU memory address writing at {offset:X}");
        }

        public ushort ReadWord(ushort offset) => (ushort)(ReadByte((ushort)(offset + 1)) << 8 | ReadByte(offset));

        public void WriteWord(ushort offset, ushort value)
        {
            WriteByte(offset, (byte)(value & 0xff));
            WriteByte((ushort)(offset + 1), (byte)(value >> 8));
        }

        public void CopyFrom(byte[] bytes) => Array.Copy(bytes, _mem, bytes.Length);

        public void CopyFrom(byte[] bytes, int destIndex)
            => Array.Copy(bytes, 0, _mem, destIndex, bytes.Length);

        public void Reset() => Array.Clear(_mem, 0, _mem.Length);
    }
}
