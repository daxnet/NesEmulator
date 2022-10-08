using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
