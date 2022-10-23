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
    /// Represents an NES component.
    /// </summary>
    public abstract class NesComponent
    {

        #region Private Fields

        private readonly Emulator _emulator;

        #endregion Private Fields

        #region Protected Constructors

        /// <summary>
        /// Initializes a new instance of the <c>NesComponent</c> class.
        /// </summary>
        /// <param name="emulator">The instance of the emulator.</param>
        protected NesComponent(Emulator emulator) => _emulator = emulator;

        #endregion Protected Constructors

        #region Protected Properties

        /// <summary>
        /// Gets the instance of the NES emulator.
        /// </summary>
        protected Emulator Emulator => _emulator;

        #endregion Protected Properties

    }
}