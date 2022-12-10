using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core
{
    public sealed class EmulatorOptions
    {
        public Stream? LogOutputStream { get; set; }

        public bool EnableLogging { get; set; } = false;

        public static readonly EmulatorOptions Default = new()
        {
            EnableLogging = false,
            LogOutputStream = null
        };
    }
}
