// ============================================================================
//       __ __   __
//  |\ ||_ (_   |_  _    | _ |_ _  _
//  | \||____)  |__||||_||(_||_(_)|
//
// Written by Sunny Chen (daxnet), 2022
// MIT License
// ============================================================================

namespace NesEmulator.Core
{
    /// <summary>
    /// Represents the CPU status flags.
    /// </summary>
    public struct StatusFlags
    {
        #region Private Fields

        private byte _flags;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <c>StatusFlags</c> struct.
        /// </summary>
        public StatusFlags() : this(0) { }

        /// <summary>
        /// Initializes a new instance of the <c>StatusFlags</c> struct.
        /// </summary>
        /// <param name="flags">The initial values for the flags.</param>
        public StatusFlags(byte flags) => _flags = flags;

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the lower bit of the B flag.
        /// </summary>
        public Bit Bit4
        {
            get => (_flags & 0x10) >> 4;
            set => _flags = Bit.SetBit(_flags, 4, value);
        }

        /// <summary>
        /// Gets or sets the higher bit of the B flag.
        /// </summary>
        public Bit Bit5
        {
            get => (_flags & 0x20) >> 5;
            set => _flags = Bit.SetBit(_flags, 5, value);
        }

        /// <summary>
        /// Gets or sets the Carry (C) flag.
        /// </summary>
        public Bit C
        {
            get => _flags & 0x1;
            set => _flags = Bit.SetBit(_flags, 0, value);
        }

        /// <summary>
        /// Gets or sets the Decimal (D) flag.
        /// </summary>
        public Bit D
        {
            get => (_flags & 0x8) >> 3;
            set => _flags = Bit.SetBit(_flags, 3, value);
        }

        public byte Flags
        {
            get => _flags;
            set => _flags = value;
        }

        /// <summary>
        /// Gets or sets the Interrupt Disable (I) flag.
        /// </summary>
        public Bit I
        {
            get => (_flags & 0x4) >> 2;
            set => _flags = Bit.SetBit(_flags, 2, value);
        }

        /// <summary>
        /// Gets or sets the Negative (N) flag.
        /// </summary>
        public Bit N
        {
            get => (_flags & 0x80) >> 7;
            set => _flags = Bit.SetBit(_flags, 7, value);
        }

        /// <summary>
        /// Gets or sets the Overflow (V) flag.
        /// </summary>
        public Bit V
        {
            get => (_flags & 0x40) >> 6;
            set => _flags = Bit.SetBit(_flags, 6, value);
        }

        /// <summary>
        /// Gets or sets the Zero (Z) flag.
        /// </summary>
        public Bit Z
        {
            get => (_flags & 0x2) >> 1;
            set => _flags = Bit.SetBit(_flags, 1, value);
        }

        #endregion Public Properties

        #region Public Methods

        /// <inheritdoc/>
        public static bool operator !=(StatusFlags left, StatusFlags right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public static bool operator ==(StatusFlags left, StatusFlags right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Clears the status flags;
        /// </summary>
        public void Clear() => _flags = 0;

        /// <inheritdoc/>
        public override bool Equals(object? obj) => obj is StatusFlags flags &&
                   _flags == flags._flags;

        /// <inheritdoc/>
        public override int GetHashCode() => HashCode.Combine(_flags);

        /// <inheritdoc/>
        public override string ToString() => Convert.ToString(_flags, 2);

        #endregion Public Methods
    }
}