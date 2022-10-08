using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core
{
    /// <summary>
    /// Represents a bit in a byte.
    /// </summary>
    public struct Bit
    {

        #region Private Fields

        private readonly int _value;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <c>Bit</c> struct.
        /// </summary>
        public Bit() : this(0)
        { }

        /// <summary>
        /// Initializes a new instance of the <c>Bit</c> struct.
        /// </summary>
        /// <param name="value">A integer value that is used to initialize the bit value.
        /// If this integer value equals to zero, the bit is cleared, otherwise, the bit
        /// is set.</param>
        public Bit(int value) => _value = value == 0 ? 0 : 1;

        /// <summary>
        /// Initializes a new instance of the <c>Bit</c> struct.
        /// </summary>
        /// <param name="value">A boolean value that is used to initialize the bit value.
        /// If the boolean value is true, the bit is set, otherwise the bit is cleared.</param>
        public Bit(bool value) => _value = value ? 1 : 0;

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the boolean representation of the bit. If the bit is set, this returns <c>true</c>,
        /// if the bit is cleared, this returns <c>false</c>.
        /// </summary>
        public bool BoolValue => _value != 0;

        /// <summary>
        /// Gets the integer representation of the bit. If the bit is set, this returns <c>1</c>,
        /// if the bit is cleared, this returns <c>0</c>.
        /// </summary>
        public int Value => _value;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Clears a bit in a given byte on the given bit position.
        /// </summary>
        /// <param name="src">The byte to be operated on.</param>
        /// <param name="bitPos">The position in the byte to be cleared.</param>
        /// <returns>A byte with the specified position being cleared.</returns>
        public static byte Clear(byte src, int bitPos) => (byte)(src & ~(1 << bitPos));

        public static implicit operator Bit(int value) => new(value);

        public static implicit operator Bit(bool value) => new(value);

        public static implicit operator bool(Bit bit) => bit._value == 1;

        public static implicit operator int(Bit bit) => bit._value;

        public static bool operator !=(Bit a, Bit b) => !(a == b);

        public static Bit operator &(Bit a, Bit b) => a._value & b._value;

        public static Bit operator ^(Bit a, Bit b) => a._value ^ b._value;

        public static Bit operator |(Bit a, Bit b) => a._value | b._value;

        public static Bit operator ~(Bit a) => !a;

        public static bool operator ==(Bit a, Bit b) => a.Value == b.Value;

        /// <summary>
        /// Sets a bit in a given byte on the given bit position.
        /// </summary>
        /// <param name="src">The byte to be operated on.</param>
        /// <param name="bitPos">The position in the byte to be set.</param>
        /// <returns>A byte with the specified position being set.</returns>
        public static byte Set(byte src, int bitPos) => (byte)(src | (1 << bitPos));

        /// <summary>
        /// Gets a bit on the given byte.
        /// </summary>
        /// <param name="src">The byte to be operated on.</param>
        /// <param name="bitPos">The position in the byte.</param>
        /// <param name="value">The bit value to be set on the byte.</param>
        /// <returns>The byte with the bit value being set.</returns>
        public static byte SetBit(byte src, int bitPos, Bit value) => value ? Set(src, bitPos) : Clear(src, bitPos);
        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is Bit bit &&
                   _value == bit._value;

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(_value);

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();

        #endregion Public Methods

    }
}
