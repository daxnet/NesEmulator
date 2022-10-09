using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core
{
    public static class Utils
    {
        public static ushort WrapAsUShort(this int val)
            => (ushort)(val % ushort.MaxValue);

        public static byte WrapAsByte(this int val)
            => (byte)(val % byte.MaxValue);
    }
}
