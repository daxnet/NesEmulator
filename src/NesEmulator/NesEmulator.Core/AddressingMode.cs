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
