// ============================================================================
//       __ __   __
//  |\ ||_ (_   |_  _    | _ |_ _  _
//  | \||____)  |__||||_||(_||_(_)|
//
// Written by Sunny Chen (daxnet), 2022
// MIT License
// ============================================================================

namespace NesEmulator.Core.Mappers
{
    [Mapper(0, "NRom Mapper")]
    internal sealed class NRomMapper : Mapper
    {
        #region Public Constructors

        public NRomMapper(Emulator emulator) : base(emulator)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override byte Read(ushort address)
        {
            byte val = 0;
            if (Emulator.InstalledCartridge != null)
            {
                if (address < 0x2000)
                {

                }
                if (address >= 0x8000)
                {
                    val = Emulator.InstalledCartridge.ReadPrgRom(ResolveAddress(address));
                }
            }

            return val;
        }

        public override void Write(ushort address, byte value)
        {
            if (address < 0x2000)
            {

            }
        }

        #endregion Public Methods

        #region Private Methods

        private ushort ResolveAddress(ushort address)
        {
            var resolvedAddress = (ushort)(address - 0x8000);
            return Emulator.InstalledCartridge?.NumOf16kPrgRomBanks == 2 ? resolvedAddress : (ushort)(resolvedAddress % 0x4000);
        }

        #endregion Private Methods
    }
}