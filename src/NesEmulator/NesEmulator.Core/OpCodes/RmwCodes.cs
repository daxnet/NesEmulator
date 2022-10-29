using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Core.OpCodes
{
    [OpCodeDefinition(AddressingMode.Accumulator, 0x0a, 1, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x06, 2, 5)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x16, 2, 6)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x0e, 3, 6)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x1e, 3, 7)]
    internal sealed class ASL : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            byte result;
            switch (opCodeDefinition.AddressingMode)
            {
                case AddressingMode.Accumulator:
                    result = CalculateAsl(cpu, cpu.A);
                    cpu.SetRegister(RegisterNames.A, result);
                    break;
                default:
                    var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
                    result = CalculateAsl(cpu, memory.ReadByte(address));
                    memory.WriteByte(address, result);
                    cpu.UpdateZeroAndNegativeFlags(result);
                    break;
            }
        }

        private static byte CalculateAsl(Cpu cpu, byte input)
        {
            cpu.StatusFlags.C = input >> 7 == 1;
            return (byte)(input << 1);
        }
    }

    [OpCodeDefinition(AddressingMode.Accumulator, 0x2a, 1, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x26, 2, 5)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x36, 2, 6)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x2e, 3, 6)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x3e, 3, 7)]
    internal sealed class ROL : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            byte result;
            switch (opCodeDefinition.AddressingMode)
            {
                case AddressingMode.Accumulator:
                    result = CalculateRol(cpu, cpu.A);
                    cpu.SetRegister(RegisterNames.A, result);
                    break;
                default:
                    var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
                    result = CalculateRol(cpu, memory.ReadByte(address));
                    memory.WriteByte(address, result);
                    cpu.UpdateZeroAndNegativeFlags(result);
                    break;
            }
        }

        private static byte CalculateRol(Cpu cpu, byte src)
        {
            var oldCarryFlag = cpu.StatusFlags.C;
            cpu.StatusFlags.C = Bit.HasSet(src, 7);
            return (byte)((src << 1) | oldCarryFlag.Value);
        }
    }

    [OpCodeDefinition(AddressingMode.Accumulator, 0x4a, 1, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x46, 2, 5)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x56, 2, 6)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x4e, 3, 6)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x5e, 3, 7)]
    internal sealed class LSR : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            byte result;
            switch (opCodeDefinition.AddressingMode)
            {
                case AddressingMode.Accumulator:
                    result = CalculateLsr(cpu, cpu.A);
                    cpu.SetRegister(RegisterNames.A, result);
                    break;
                default:
                    var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
                    result = CalculateLsr(cpu, memory.ReadByte(address));
                    memory.WriteByte(address, result);
                    cpu.UpdateZeroAndNegativeFlags(result);
                    break;
            }
        }

        private static byte CalculateLsr(Cpu cpu, byte input)
        {
            cpu.StatusFlags.C = Bit.HasSet(input, 0);
            return (byte)(input >> 1);
        }
    }

    [OpCodeDefinition(AddressingMode.Accumulator, 0x6a, 1, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0x66, 2, 5)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0x76, 2, 6)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x6e, 3, 6)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0x7e, 3, 7)]
    internal sealed class ROR : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            byte result;
            switch (opCodeDefinition.AddressingMode)
            {
                case AddressingMode.Accumulator:
                    result = CalculateRor(cpu, cpu.A);
                    cpu.SetRegister(RegisterNames.A, result);
                    break;
                default:
                    var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
                    result = CalculateRor(cpu, memory.ReadByte(address));
                    memory.WriteByte(address, result);
                    cpu.UpdateZeroAndNegativeFlags(result);
                    break;
            }
        }

        private static byte CalculateRor(Cpu cpu, byte src)
        {
            var oldCarryFlag = cpu.StatusFlags.C;
            cpu.StatusFlags.C = Bit.HasSet(src, 0);
            return (byte)((src >> 1) | (oldCarryFlag.Value << 7));
        }
    }

    [OpCodeDefinition(AddressingMode.ZeroPage, 0x86, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageY, 0x96, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0x8e, 3, 4)]
    internal sealed class STX : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            memory.WriteByte(address, cpu.X);
        }
    }

    [OpCodeDefinition(AddressingMode.Implicit, 0x8a, 1, 2)]
    internal sealed class TXA : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.SetRegister(RegisterNames.A, cpu.X);
        }
    }

    [OpCodeDefinition(AddressingMode.Implicit, 0x9a, 1, 2)]
    internal sealed class TXS : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.SP = cpu.X;
        }
    }

    [OpCodeDefinition(AddressingMode.Immediate, 0xa2, 2, 2)]
    [OpCodeDefinition(AddressingMode.ZeroPage, 0xa6, 2, 3)]
    [OpCodeDefinition(AddressingMode.ZeroPageY, 0xb6, 2, 4)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xae, 3, 4)]
    [OpCodeDefinition(AddressingMode.AbsoluteY, 0xbe, 3, 4)]
    internal sealed class LDX : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var byteVal = memory.ReadByte(address);
            cpu.SetRegister(RegisterNames.X, byteVal);
        }
    }

    [OpCodeDefinition(AddressingMode.Implicit, 0xaa, 1, 2)]
    internal sealed class TAX : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.SetRegister(RegisterNames.X, cpu.A);
        }
    }

    [OpCodeDefinition(AddressingMode.Implicit, 0xba, 1, 2)]
    internal sealed class TSX : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            cpu.SetRegister(RegisterNames.X, cpu.SP);
        }
    }

    [OpCodeDefinition(AddressingMode.ZeroPage, 0xc6, 2, 5)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xd6, 2, 6)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xce, 3, 6)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0xde, 3, 7)]
    internal sealed class DEC : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var m = memory.ReadByte(address);
            m--;
            memory.WriteByte(address, m);
            cpu.UpdateZeroAndNegativeFlags(m);
        }
    }

    [OpCodeDefinition(AddressingMode.Implicit, 0xca, 1, 2)]
    internal sealed class DEX : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
            => cpu.SetRegister(RegisterNames.X, (byte)(cpu.X - 1));
    }

    [OpCodeDefinition(AddressingMode.ZeroPage, 0xe6, 2, 5)]
    [OpCodeDefinition(AddressingMode.ZeroPageX, 0xf6, 2, 6)]
    [OpCodeDefinition(AddressingMode.Absolute, 0xee, 3, 6)]
    [OpCodeDefinition(AddressingMode.AbsoluteX, 0xfe, 3, 7)]
    internal sealed class INC : OpCode
    {
        protected override void DoExecute(Cpu cpu, Memory memory, OpCodeDefinitionAttribute opCodeDefinition)
        {
            var address = cpu.GetOperandAddress(opCodeDefinition.AddressingMode);
            var b = memory.ReadByte(address);
            b++;
            memory.WriteByte(address, b);
            cpu.UpdateZeroAndNegativeFlags(b);
        }
    }
}
