// ============================================================================
//       __ __   __
//  |\ ||_ (_   |_  _    | _ |_ _  _
//  | \||____)  |__||||_||(_||_(_)|
//
// Written by Sunny Chen (daxnet), 2022
// MIT License
// ============================================================================

namespace NesEmulator.Core.OpCodes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class OpCodeDefinitionAttribute : Attribute
    {
        #region Public Constructors

        public OpCodeDefinitionAttribute(AddressingMode addressingMode, byte opCode, ushort bytes, ushort cycles, bool pageCrossCheck = false, bool unofficial = false)
        {
            AddressingMode = addressingMode;
            OpCode = opCode;
            Bytes = bytes;
            Cycles = cycles;
            Unofficial = unofficial;
            PageCrossCheck = pageCrossCheck;
        }

        #endregion Public Constructors

        #region Public Properties

        public AddressingMode AddressingMode { get; init; }

        public ushort Bytes { get; init; }
        public ushort Cycles { get; init; }
        public byte OpCode { get; init; }
        public bool PageCrossCheck { get; init; }
        public bool Unofficial { get; init; }

        #endregion Public Properties

        #region Public Methods

        public override string ToString() => $"{OpCode:x} {AddressingMode} {Cycles}";

        #endregion Public Methods
    }
}