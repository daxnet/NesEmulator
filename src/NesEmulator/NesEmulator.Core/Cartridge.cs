using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core
{
    public sealed class Cartridge
    {
        private readonly string _fileName;
        private readonly byte[] _raw;

        public Cartridge(string fileName)
        {
            _fileName = fileName;
            _raw = File.ReadAllBytes(_fileName);
        }

        public string FileName => _fileName;

        public override string ToString() => _fileName;
    }
}
