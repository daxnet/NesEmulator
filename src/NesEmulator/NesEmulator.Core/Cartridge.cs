using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core
{
    public sealed class Cartridge
    {
        private const int PRG_ROM_UNIT_SIZE = 0x4000;
        private const int CHR_ROM_UNIT_SIZE = 0x2000;
        private const int PRG_RAM_UNIT_SIZE = 0x2000;
        private const int NES_HEADER_VALUE = 0x1A53454E;

        private readonly string? _fileName;
        private readonly byte[] _raw;
        private readonly int _prgRomSize;
        private readonly int _chrRomSize;
        private readonly int _prgRamSize;
        private readonly bool _verticalMirroring;
        private readonly bool _isBatteryBacked;
        private readonly bool _hasTrainer;
        private readonly bool _fourScreenVramLayout;
        private readonly Version _version;
        private readonly int _mapperType;
        private readonly byte[] _prgRom;
        private readonly byte[] _chrRom;

        public Cartridge(string fileName) : this(File.ReadAllBytes(fileName)) => _fileName = fileName;

        public Cartridge(byte[] rom)
        {
            _raw = new byte[rom.Length];
            Array.Copy(rom, _raw, rom.Length);

            // Checks if the file is in NES format.
            if (BitConverter.ToInt32(_raw) != NES_HEADER_VALUE)
            {
                throw new FormatException("The file is not in a correct format.");
            }

            _version = (_raw[7] & 0xc) >> 2 == 0x10 ? new Version(2, 0) : new Version(1, 0);

            _prgRomSize = _raw[4] * PRG_ROM_UNIT_SIZE;
            _chrRomSize = _raw[5] * CHR_ROM_UNIT_SIZE;
            _prgRamSize = _raw[8] * PRG_RAM_UNIT_SIZE;
            _verticalMirroring = Bit.HasSet(_raw[6], 0);
            _isBatteryBacked = Bit.HasSet(_raw[6], 1);
            _hasTrainer = Bit.HasSet(_raw[6], 2);
            _fourScreenVramLayout = Bit.HasSet(_raw[6], 3);

            _mapperType = (_raw[7] & 0xf0) | (_raw[6] & 0xf);

            var prg_rom_begin_pos = 16 + (_hasTrainer ? 512 : 0);
            var chr_rom_begin_pos = prg_rom_begin_pos + _prgRomSize;

            _prgRom = new byte[_prgRomSize];
            Array.Copy(_raw, prg_rom_begin_pos, _prgRom, 0, _prgRomSize);

            _chrRom = new byte[_chrRomSize];
            Array.Copy(_raw, chr_rom_begin_pos, _chrRom, 0, _chrRomSize);
        }

        public string? FileName => _fileName;

        public int PrgRomSize => _prgRomSize;

        public int ChrRomSize => _chrRomSize;

        public int PrgRamSize => _prgRamSize;

        public bool IsBatteryBacked => _isBatteryBacked;

        public bool HasTrainer => _hasTrainer;

        public bool FourScreenVramLayout => _fourScreenVramLayout;

        public Version Version => _version;

        public int MapperType => _mapperType;

        /// <summary>
        /// Gets a <see cref="bool"/> value which indicates the mirroring type.
        /// <c>true</c> if it is in vertical mirroring, <c>false</c> represents
        /// the horizontal mirroring.
        /// </summary>
        public bool VerticalMirroring => _verticalMirroring;

        public byte[] PrgRom => _prgRom;

        public byte[] ChrRom => _chrRom;

        public override string? ToString() => _fileName;
    }
}
