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
    [OpCodeDefinition(AddressingMode.Immediate, 0x0b, 2, 2, true)]
    [OpCodeDefinition(AddressingMode.Immediate, 0x2b, 2, 2, true)]
    internal sealed class ANC : OpCode
    {
        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var result = (byte)(memory.ReadByte(address) & cpu.A);
            cpu.SetRegister(RegisterNames.A, result);
            cpu.StatusFlags.C = cpu.StatusFlags.N;
        }

        #endregion Protected Methods
    }

    [OpCodeDefinition(AddressingMode.Immediate, 0x6b, 2, 2, true)]
    internal sealed class ARR : OpCode
    {
        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var byteVal = memory.ReadByte(address);
            cpu.A &= byteVal;
            cpu.SetRegister(RegisterNames.A, (byte)(((cpu.A & 1) << 7) | (cpu.A >> 1)));
            cpu.StatusFlags.C = Bit.HasSet(cpu.A, 6);
            cpu.StatusFlags.V = Bit.GetBit(cpu.A, 6) ^ Bit.GetBit(cpu.A, 5);
        }

        #endregion Protected Methods
    }

    [OpCodeDefinition(AddressingMode.ZeroPage, 0x87, 2, 3, true)]
    [OpCodeDefinition(AddressingMode.ZeroPageY, 0x97, 2, 4, true)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0x83, 2, 6, true)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x8f, 3, 4, true)]
    internal sealed class SAX : OpCode
    {
        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var result = (byte)(cpu.A & cpu.X);
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            memory.WriteByte(address, result);
            cpu.UpdateZeroAndNegativeFlags(result);
        }

        #endregion Protected Methods
    }

    [OpCodeDefinition(AddressingMode.Immediate, 0x4b, 2, 2, true)]
    internal sealed class ASR : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var byteVal = memory.ReadByte(address);
            cpu.A &= byteVal;
            cpu.StatusFlags.C = Bit.HasSet(cpu.A, 0);
            cpu.SetRegister(RegisterNames.A, (byte)(cpu.A >> 1));
        }
    }

    [OpCodeDefinition(AddressingMode.Immediate, 0xab, 2, 2, true)]
    internal sealed class ATX : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var byteVal = memory.ReadByte(address);
            cpu.SetRegister(RegisterNames.A, (byte)(cpu.A & byteVal));
            cpu.SetRegister(RegisterNames.X, cpu.A);
        }
    }

    [OpCodeDefinition(AddressingMode.AbsoluteY, 0x9f, 3, 5, true)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0x93, 2, 6, true)]
    internal sealed class AXA : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var val = cpu.X & cpu.A & 7;
            memory.WriteByte(address, (byte)val);
        }
    }

    [OpCodeDefinition(AddressingMode.Immediate, 0xcb, 2, 2, true)]
    internal sealed class AXS : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            cpu.X &= cpu.A;
            var byteVal = memory.ReadByte(address);
            if (byteVal > cpu.X)
            {
                cpu.StatusFlags.C = 1;
            }
            cpu.SetRegister(RegisterNames.X, (byte)(cpu.X - byteVal));
        }
    }

    [OpCodeDefinition(AddressingMode.ZeroPage, 0xc7, 2, 5, true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xd7, 2, 6, true)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xcf, 3, 6, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0xdf, 3, 7, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0xdb, 3, 7, true)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0xc3, 2, 8, true)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0xd3, 2, 8, true)]
    internal sealed class DCP : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var m = memory.ReadByte(address);
            m--;
            memory.WriteByte(address, m);
            var result = cpu.A - m;
            if (result >= 0)
            {
                cpu.StatusFlags.C = true;
                if (result == 0)
                {
                    cpu.StatusFlags.Z = true;
                }
            }
            else
            {
                cpu.StatusFlags.N = true;
            }
        }
    }

    [OpCodeDefinition(AddressingMode.ZeroPage, 0x4, 2, 3, true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x14, 2, 4, true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x34, 2, 4, true)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x44, 2, 3, true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x54, 2, 4, true)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x64, 2, 3, true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x74, 2, 4, true)]
    [OpCodeDefinition(AddressingMode.Immediate, 0x80, 2, 2, true)]
    [OpCodeDefinition(AddressingMode.Immediate, 0x82, 2, 2, true)]
    [OpCodeDefinition(AddressingMode.Immediate, 0x89, 2, 2, true)]
    [OpCodeDefinition(AddressingMode.Immediate, 0xc2, 2, 2, true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xd4, 2, 4, true)]
    [OpCodeDefinition(AddressingMode.Immediate, 0xe2, 2, 2, true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xf4, 2, 4, true)]
    internal sealed class DOP : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition) { }
    }

    [OpCodeDefinition(AddressingMode.ZeroPage, 0xe7, 2, 5, true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xf7, 2, 6, true)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xef, 3, 6, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0xff, 3, 7, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0xfb, 3, 7, true)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0xe3, 2, 8, true)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0xf3, 2, 8, true)]
    internal sealed class ISC : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            // INC
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var b = memory.ReadByte(address);
            b++;
            memory.WriteByte(address, b);

            // SBC
            cpu.AddToRegisterA((byte)~b);
        }
    }

    [OpCodeDefinition(AddressingMode.Implicit, 0x2, 1, 0, true)]
    [OpCodeDefinition(AddressingMode.Implicit, 0x12, 1, 0, true)]
    [OpCodeDefinition(AddressingMode.Implicit, 0x22, 1, 0, true)]
    [OpCodeDefinition(AddressingMode.Implicit, 0x32, 1, 0, true)]
    [OpCodeDefinition(AddressingMode.Implicit, 0x42, 1, 0, true)]
    [OpCodeDefinition(AddressingMode.Implicit, 0x52, 1, 0, true)]
    [OpCodeDefinition(AddressingMode.Implicit, 0x62, 1, 0, true)]
    [OpCodeDefinition(AddressingMode.Implicit, 0x72, 1, 0, true)]
    [OpCodeDefinition(AddressingMode.Implicit, 0x92, 1, 0, true)]
    [OpCodeDefinition(AddressingMode.Implicit, 0xb2, 1, 0, true)]
    [OpCodeDefinition(AddressingMode.Implicit, 0xd2, 1, 0, true)]
    [OpCodeDefinition(AddressingMode.Implicit, 0xf2, 1, 0, true)]
    internal sealed class KIL : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition) { }

        protected override void IncreaseProgramCounter(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition)
        {
            // Stop program counter
        }
    }

    [OpCodeDefinition(AddressingMode.AbsoluteY, 0xbb, 3, 4, true)]
    internal sealed class LAR : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var byteVal = memory.ReadByte(address);
            cpu.SP &= byteVal;
            cpu.SetRegister(RegisterNames.A, cpu.SP);
            cpu.SetRegister(RegisterNames.X, cpu.SP);
        }
    }

    [OpCodeDefinition(AddressingMode.ZeroPage, 0xa7, 2, 3, true)]
    [OpCodeDefinition(AddressingMode.ZeroPageY, 0xb7, 2, 4, true)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xaf, 3, 4, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0xbf, 3, 4, true)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0xa3, 2, 6, true)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0xb3, 2, 5, true)]
    internal sealed class LAX : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            // LDA
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var byteVal = memory.ReadByte(address);
            cpu.SetRegister(RegisterNames.A, byteVal);

            // TAX
            cpu.SetRegister(RegisterNames.X, cpu.A);
        }
    }

    [OpCodeDefinition(AddressingMode.ZeroPage, 0x27, 2, 5, true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x37, 2, 6, true)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x2f, 3, 6, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x3f, 3, 7, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0x3b, 3, 7, true)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0x23, 2, 8, true)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0x33, 2, 8, true)]
    internal sealed class RLA : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var byteVal = memory.ReadByte(address);
            var oldCarryFlag = cpu.StatusFlags.C;
            cpu.StatusFlags.C = Bit.HasSet(byteVal, 7);
            byteVal = (byte)((byteVal << 1) | oldCarryFlag.Value);
            memory.WriteByte(address, byteVal);
            cpu.SetRegister(RegisterNames.A, (byte)(cpu.A & byteVal));
        }
    }

    [OpCodeDefinition(AddressingMode.ZeroPage, 0x67, 2, 5, true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x77, 2, 6, true)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x6f, 3, 6, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x7f, 3, 7, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0x7b, 3, 7, true)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0x63, 2, 8, true)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0x73, 2, 8, true)]
    internal sealed class RRA : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var byteVal = memory.ReadByte(address);
            var oldCarryFlag = cpu.StatusFlags.C;
            cpu.StatusFlags.C = Bit.HasSet(byteVal, 0);
            byteVal = (byte)((byteVal >> 1) | (oldCarryFlag.Value << 7));
            memory.WriteByte(address, byteVal);
            cpu.AddToRegisterA(byteVal);
        }
    }

    [OpCodeDefinition(AddressingMode.ZeroPage, 0x7, 2, 5, true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x17, 2, 6, true)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xf, 3, 6, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x1f, 3, 7, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0x1b, 3, 7, true)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0x3, 2, 8, true)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0x13, 2, 8, true)]
    internal sealed class SLO : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var byteVal = memory.ReadByte(address);
            cpu.StatusFlags.C = byteVal >> 7 == 1;
            byteVal = (byte)(byteVal << 1);
            memory.WriteByte(address, byteVal);
            cpu.SetRegister(RegisterNames.A, (byte)(cpu.A | byteVal));
        }
    }

    [OpCodeDefinition(AddressingMode.ZeroPage, 0x47, 2, 5, true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x57, 2, 6, true)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x4f, 3, 6, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x5f, 3, 7, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0x5b, 3, 7, true)]
    [OpCodeDefinition(AddressingMode.IndexedIndirect, 0x43, 2, 8, true)]
    [OpCodeDefinition(AddressingMode.IndirectIndexed, 0x53, 2, 8, true)]
    internal sealed class SRE : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var byteVal = memory.ReadByte(address);
            cpu.StatusFlags.C = Bit.HasSet(byteVal, 0);
            byteVal = (byte)(byteVal >> 1);
            memory.WriteByte(address, byteVal);
            cpu.SetRegister(RegisterNames.A, (byte)(cpu.A ^ byteVal));
        }
    }
}