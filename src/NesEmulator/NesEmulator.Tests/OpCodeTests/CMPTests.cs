﻿using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests.OpCodeTests
{
    public class CMPTests
    {
        private readonly Emulator emulator = new();

        [Test]
        public void ZeroflagTest()
        {
            var program = new byte[] { 0xc9, 0x50 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x50);
            Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)1));
        }

        [Test]
        public void ZeroflagClearTest()
        {
            var program = new byte[] { 0xc9, 0x50 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x51);
            Assert.That(emulator.Cpu.StatusFlags.Z, Is.EqualTo((Bit)0));
        }

        [Test]
        public void CarryflagTest()
        {
            var program = new byte[] { 0xc9, 0x50 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x50);
            Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)1));
        }

        [Test]
        public void Carryflag2Test()
        {
            var program = new byte[] { 0xc9, 0x50 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x51);
            Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)1));
        }

        [Test]
        public void CarryflagClearTest()
        {
            var program = new byte[] { 0xc9, 0x50 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x49);
            Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)0));
        }

        [Test]
        public void NegativeflagTest()
        {
            var program = new byte[] { 0xc9, 0x50 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x49);
            Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)1));
        }

        [Test]
        public void NegativeflagClearTest()
        {
            var program = new byte[] { 0xc9, 0x50 };
            emulator.Cpu.LoadAndRun(program, e => e.Cpu.A = 0x51);
            Assert.That(emulator.Cpu.StatusFlags.N, Is.EqualTo((Bit)0));
        }

        [Test]
        public void IndirectIndexedAddressingTest()
        {
            // LDA #$50
            // STA $8110
            // LDA #$0E
            // STA $05
            // LDA #$81
            // STA $06
            // LDY #$2
            // LDA #$50
            // CMP ($5),Y
            TestUtils.SetMemoryByte(emulator.Memory, 0x8110, 0x50);
            TestUtils.SetMemoryByte(emulator.Memory, 0x05, 0x0e);
            TestUtils.SetMemoryByte(emulator.Memory, 0x06, 0x81);
            var program = new byte[] { 0xd1, 0x05 };
            emulator.Cpu.LoadAndRun(program, e =>
            {
                e.Cpu.Y = 0x02;
                e.Cpu.A = 0x50;
            });
            Assert.That(emulator.Cpu.StatusFlags.C, Is.EqualTo((Bit)1));
        }
    }
}
