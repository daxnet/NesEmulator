using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class OpCodeDefinitionAttribute : Attribute
    {
        public OpCodeDefinitionAttribute(AddressingMode addressingMode, byte opCode, ushort bytes, ushort cycles, bool unofficial = false)
        {
            AddressingMode = addressingMode;
            OpCode = opCode;
            Bytes = bytes;
            Cycles = cycles;
            Unofficial = unofficial;
        }

        public AddressingMode AddressingMode { get; init; }

        public byte OpCode { get; init; }

        public ushort Cycles { get; init; }

        public ushort Bytes { get; init; }

        public bool Unofficial { get; init; }

        public override string ToString() => $"{OpCode:x} {AddressingMode} {Cycles}";
    }
}
