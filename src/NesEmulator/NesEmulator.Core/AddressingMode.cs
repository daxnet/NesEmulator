using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core
{
    /// <summary>
    /// Represents the CPU addressing modes.
    /// </summary>
    public enum AddressingMode
    {
        Implicit,
        /// <summary>
        /// The operation is performed on the accumulator
        /// rather than on the memory block.
        /// </summary>
        Accumulator,
        Immediate,
        ZeroPage,
        ZeroPageX,
        ZeroPageY,
        Relative,
        Absolute,
        AbsoluteX,
        AbsoluteY,
        Indirect,
        /// <summary>
        /// Indexed Indirect Mode (or Indirect.X)
        /// </summary>
        IndexedIndirect,
        /// <summary>
        /// Indirect Indexed Mode (or Indirect.Y)
        /// </summary>
        IndirectIndexed,
    }
}
