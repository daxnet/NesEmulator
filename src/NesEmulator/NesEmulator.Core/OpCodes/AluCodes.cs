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
    [OpCodeDefinition(AddressingMode.Immediate, 0x69, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x65, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x75, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x6d, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x7d, 3, 4, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0x79, 3, 4, true)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0x61, 2, 6)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0x71, 2, 5, true)]
    internal sealed class ADC : OpCode
    {
        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var val = memory.ReadByte(address);
            cpu.AddToRegisterA(val);
        }

        #endregion Protected Methods
    }

    [OpCodeDefinition(AddressingMode.Immediate, 0x29, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x25, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x35, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x2d, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x3d, 3, 4, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0x39, 3, 4, true)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0x21, 2, 6)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0x31, 2, 5, true)]
    internal sealed class AND : OpCode
    {
        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var val = memory.ReadByte(address);
            cpu.SetRegister(RegisterNames.A, (byte)(cpu.A & val));
        }

        #endregion Protected Methods
    }

    [OpCodeDefinition(AddressingMode.Immediate, 0xc9, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0xc5, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xd5, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xcd, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0xdd, 3, 4, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0xd9, 3, 4, true)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0xc1, 2, 6)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0xd1, 2, 5, true)]
    internal sealed class CMP : OpCode
    {
        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var val = memory.ReadByte(address);
            var result = cpu.A - val;
            cpu.StatusFlags.Z = result == 0;
            cpu.StatusFlags.C = result >= 0;
            cpu.StatusFlags.N = (result & 0x80) > 0 && result != 0;
        }

        #endregion Protected Methods
    }

    [OpCodeDefinition(AddressingMode.Immediate, 0x49, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x45, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x55, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x4d, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x5d, 3, 4, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0x59, 3, 4, true)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0x41, 2, 6)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0x51, 2, 5, true)]
    internal sealed class EOR : OpCode
    {
        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var b = memory.ReadByte(address);
            cpu.SetRegister(RegisterNames.A, (byte)(cpu.A ^ b));
        }

        #endregion Protected Methods
    }

    [OpCodeDefinition(AddressingMode.Immediate, 0xa9, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0xa5, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xb5, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xad, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0xbd, 3, 4, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0xb9, 3, 4, true)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0xa1, 2, 6)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0xb1, 2, 5, true)]
    internal sealed class LDA : OpCode
    {
        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var byteVal = memory.ReadByte(address);
            cpu.SetRegister(RegisterNames.A, byteVal);
        }

        #endregion Protected Methods
    }

    [OpCodeDefinition(AddressingMode.Immediate, 0x9, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x5, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x15, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xd, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x1d, 3, 4, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0x19, 3, 4, true)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0x1, 2, 6)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0x11, 2, 5, true)]
    internal sealed class ORA : OpCode
    {
        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var byteVal = memory.ReadByte(address);
            cpu.SetRegister(RegisterNames.A, (byte)(cpu.A | byteVal));
        }

        #endregion Protected Methods
    }

    [OpCodeDefinition(AddressingMode.Immediate, 0xe9, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0xe5, 2, 3)]
    [OpCodeDefinition(AddressingMode.Immediate, 0xeb, 2, 2, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xf5, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xed, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0xfd, 3, 4, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0xf9, 3, 4, true)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0xe1, 2, 6)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0xf1, 2, 5, true)]
    internal sealed class SBC : OpCode
    {
        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var val = memory.ReadByte(address);
            cpu.AddToRegisterA((byte)~val);
        }

        #endregion Protected Methods
    }

    [OpCodeDefinition(AddressingMode.ZeroPage, 0x85, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x95, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x8d, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x9d, 3, 5)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0x99, 3, 5)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0x81, 2, 6)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0x91, 2, 6)]
    internal sealed class STA : OpCode
    {
        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            memory.WriteByte(address, cpu.A);
        }

        #endregion Protected Methods
    }
}