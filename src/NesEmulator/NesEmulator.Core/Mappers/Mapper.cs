using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.Mappers
{
    public abstract class Mapper : NesComponent
    {
        protected Mapper(Emulator emulator) : base(emulator) { }

        public abstract byte Read(ushort address);

        public abstract void Write(ushort address, byte value);

        public int MapperId => GetType().GetCustomAttribute<MapperAttribute>()?.Id ?? int.MinValue;
    }
}
