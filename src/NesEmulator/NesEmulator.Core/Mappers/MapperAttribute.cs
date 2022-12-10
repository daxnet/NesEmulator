using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.Mappers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal sealed class MapperAttribute : Attribute
    {
        public MapperAttribute(int id) => Id = id;

        public int Id { get; init; }
    }
}
