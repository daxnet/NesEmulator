using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.Mappers
{
    /// <summary>
    /// Represents the mapper that accesses the memory directly.
    /// </summary>
    [Mapper(-1, "Direct Memory Access Mapper")]
    internal sealed class DMAMapper : Mapper
    {
        public DMAMapper(Emulator emulator) : base(emulator)
        {
        }

        public override byte Read(ushort address) => Emulator.Memory.Mem[address];

        public override void Write(ushort address, byte value) => Emulator.Memory.Mem[address] = value;

    }
}
