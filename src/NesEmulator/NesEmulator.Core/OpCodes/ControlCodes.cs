﻿// ============================================================================
//       __ __   __
//  |\ ||_ (_   |_  _    | _ |_ _  _
//  | \||____)  |__||||_||(_||_(_)|
//
// Written by Sunny Chen (daxnet), 2022
// MIT License
// ============================================================================

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Relative, 0x90, 2, 2)]
    internal sealed class BCC : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            if (!cpu.StatusFlags.C)
            {
                cpu.Branch();
            }
            else
            {
                base.IncreaseProgramCounter(cpu, opCodeDefinition);
            }
        }

        protected override void IncreaseProgramCounter(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition)
        { }
        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Relative, 0xb0, 2, 2)]
    internal sealed class BCS : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            if (cpu.StatusFlags.C)
            {
                cpu.Branch();
            }
            else
            {
                base.IncreaseProgramCounter(cpu, opCodeDefinition);
            }
        }

        protected override void IncreaseProgramCounter(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition)
        { }
        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Relative, 0xf0, 2, 2)]
    internal sealed class BEQ : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            if (cpu.StatusFlags.Z)
            {
                cpu.Branch();
            }
            else
            {
                base.IncreaseProgramCounter(cpu, opCodeDefinition);
            }
        }

        protected override void IncreaseProgramCounter(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition)
        { }
        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.ZeroPage, 0x24, 2, 3)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x2c, 3, 4)]
    internal sealed class BIT : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var val = memory.ReadByte(address);
            var result = (byte)(val & cpu.A);
            cpu.StatusFlags.Z = result == 0;
            cpu.StatusFlags.V = Bit.GetBit(val, 6);
            cpu.StatusFlags.N = Bit.GetBit(val, 7);
        }

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Relative, 0x30, 2, 2)]
    internal sealed class BMI : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            if (cpu.StatusFlags.N)
            {
                cpu.Branch();
            }
            else
            {
                base.IncreaseProgramCounter(cpu, opCodeDefinition);
            }
        }

        protected override void IncreaseProgramCounter(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition)
        { }
        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Relative, 0xd0, 2, 2)]
    internal sealed class BNE : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            if (!cpu.StatusFlags.Z)
            {
                cpu.Branch();
            }
            else
            {
                base.IncreaseProgramCounter(cpu, opCodeDefinition);
            }
        }

        protected override void IncreaseProgramCounter(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition)
        { }
        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Relative, 0x10, 2, 2)]
    internal sealed class BPL : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            if (!cpu.StatusFlags.N)
            {
                cpu.Branch();
            }
            else
            {
                base.IncreaseProgramCounter(cpu, opCodeDefinition);
            }
        }

        protected override void IncreaseProgramCounter(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition)
        { }
        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0x00, 1, 7)]
    internal sealed class BRK : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.PushWord(cpu.PC);

            // In the byte pushed, bit 5 is always set to 1,
            // and bit 4 is 1 if from an instruction (PHP or BRK)
            // or 0 if from an interrupt line being pulled low (/IRQ or /NMI).
            var flags = (byte)(cpu.StatusFlags.Flags | 0b0011_0000);
            cpu.PushByte(flags);
            cpu.PC = memory.ReadWord(0xfffe);
            cpu.StatusFlags.I = true;
        }

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Relative, 0x50, 2, 2)]
    internal sealed class BVC : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            if (!cpu.StatusFlags.V)
            {
                cpu.Branch();
            }
            else
            {
                base.IncreaseProgramCounter(cpu, opCodeDefinition);
            }
        }

        protected override void IncreaseProgramCounter(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition)
        { }
        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Relative, 0x70, 2, 2)]
    internal sealed class BVS : OpCode
    {
        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            if (cpu.StatusFlags.V)
            {
                cpu.Branch();
            }
            else
            {
                base.IncreaseProgramCounter(cpu, opCodeDefinition);
            }
        }

        protected override void IncreaseProgramCounter(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition)
        { }
        #endregion Protected Methods
    }
    [OpCodeDefinition(AddressingMode.Implicit, 0x18, 1, 2)]
    internal sealed class CLC : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
            => cpu.StatusFlags.C = false;

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0xd8, 1, 2)]
    internal sealed class CLD : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.StatusFlags.D = 0;
        }

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0x58, 1, 2)]
    internal sealed class CLI : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.StatusFlags.I = 0;
        }

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0xb8, 1, 2)]
    internal sealed class CLV : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.StatusFlags.V = 0;
        }

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Immediate, 0xe0, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0xe4, 2, 3)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xec, 3, 4)]
    internal sealed class CPX : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var val = memory.ReadByte(address);
            var result = cpu.X - val;
            cpu.StatusFlags.Z = result == 0;
            cpu.StatusFlags.C = result >= 0;
            cpu.StatusFlags.N = (result & 0x80) > 0 && result != 0;
        }

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Immediate, 0xc0, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0xc4, 2, 3)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xcc, 3, 4)]
    internal sealed class CPY : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var val = memory.ReadByte(address);
            var result = cpu.Y - val;
            cpu.StatusFlags.Z = result == 0;
            cpu.StatusFlags.C = result >= 0;
            cpu.StatusFlags.N = (result & 0x80) > 0 && result != 0;
        }

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0x88, 1, 2)]
    internal sealed class DEY : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
            => cpu.SetRegister(RegisterNames.Y, (byte)(cpu.Y - 1));

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0xe8, 1, 2)]
    internal sealed class INX : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
            => cpu.SetRegister(RegisterNames.X, (byte)(cpu.X + 1));

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0xc8, 1, 2)]
    internal sealed class INY : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
            => cpu.SetRegister(RegisterNames.Y, (byte)(cpu.Y + 1));

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Absolute, 0x4c, 3, 3)]
    [OpCodeDefinition(AddressingMode.Indirect, 0x6c, 3, 5)]
    internal sealed class JMP : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
            => cpu.PC = cpu.GetCurrentOperandAddress(opCodeDefinition.AddressingMode, out _);

        protected override void IncreaseProgramCounter(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition)
        { }
        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Absolute, 0x20, 3, 6)]
    internal sealed class JSR : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.PushWord((ushort)(cpu.PC + 2 - 1));
            cpu.PC = address;
        }

        protected override void IncreaseProgramCounter(Cpu cpu, OpCodeDefinitionAttribute opCodeDefinition)
        { }
        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Immediate, 0xa0, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0xa4, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xb4, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xac, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0xbc, 3, 4, true)]
    internal sealed class LDY : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var byteVal = memory.ReadByte(address);
            cpu.SetRegister(RegisterNames.Y, byteVal);
        }

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0xea, 1, 2)]
    [OpCodeDefinition(AddressingMode.Implicit, 0x1a, 1, 2, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.Implicit, 0x3a, 1, 2, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.Implicit, 0x5a, 1, 2, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.Implicit, 0x7a, 1, 2, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.Implicit, 0xda, 1, 2, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.Implicit, 0xfa, 1, 2, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x4, 2, 3, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x14, 2, 4, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x34, 2, 4, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x44, 2, 3, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x54, 2, 4, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x64, 2, 3, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x74, 2, 4, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.Immediate, 0x80, 2, 2, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.Immediate, 0x82, 2, 2, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.Immediate, 0x89, 2, 2, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.Immediate, 0xc2, 2, 2, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xd4, 2, 4, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.Immediate, 0xe2, 2, 2, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xf4, 2, 4, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x0c, 3, 4, Unofficial = true)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x1c, 3, 4, true, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x3c, 3, 4, true, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x5c, 3, 4, true, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x7c, 3, 4, true, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0xdc, 3, 4, true, true)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0xfc, 3, 4, true, true)]
    internal sealed class NOP : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            // Do nothing
        }

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0x48, 1, 3)]
    internal sealed class PHA : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.PushByte(cpu.A);
        }

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0x8, 1, 3)]
    internal sealed class PHP : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            // In the byte pushed, bit 5 is always set to 1,
            // and bit 4 is 1 if from an instruction (PHP or BRK)
            // or 0 if from an interrupt line being pulled low (/IRQ or /NMI).
            var val = (byte)(cpu.StatusFlags.Flags | 0b0011_0000);
            cpu.PushByte(val);
        }

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0x68, 1, 4)]
    internal sealed class PLA : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.SetRegister(RegisterNames.A, cpu.PopByte());
        }

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0x28, 1, 4)]
    internal sealed class PLP : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.StatusFlags.Flags = cpu.PopByte();
            cpu.StatusFlags.Bit4 = 0;
            cpu.StatusFlags.Bit5 = 1;
        }

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0x40, 1, 6)]
    internal sealed class RTI : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.StatusFlags.Flags = cpu.PopByte();
            cpu.StatusFlags.Bit4 = 0;
            cpu.StatusFlags.Bit5 = 1;
            cpu.PC = cpu.PopWord();
        }

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0x60, 1, 6)]
    internal sealed class RTS : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.PC = (ushort)(cpu.PopWord() + 1);
        }

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0x38, 1, 2)]
    internal sealed class SEC : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
            => cpu.StatusFlags.C = true;

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0xf8, 1, 2)]
    internal sealed class SED : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
            => cpu.StatusFlags.D = 1;

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0x78, 1, 2)]
    internal sealed class SEI : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
            => cpu.StatusFlags.I = true;

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.ZeroPage, 0x84, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x94, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x8c, 3, 4)]
    internal sealed class STY : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            memory.WriteByte(address, cpu.Y);
        }

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0xa8, 1, 2)]
    internal sealed class TAY : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.SetRegister(RegisterNames.Y, cpu.A);
        }

        #endregion Protected Methods

    }

    [OpCodeDefinition(AddressingMode.Implicit, 0x98, 1, 2)]
    internal sealed class TYA : OpCode
    {

        #region Protected Methods

        protected override void DoExecute(Cpu cpu, Memory memory, ushort address, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.SetRegister(RegisterNames.A, cpu.Y);
        }

        #endregion Protected Methods

    }
}