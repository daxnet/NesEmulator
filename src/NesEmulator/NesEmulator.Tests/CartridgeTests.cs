using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests
{
    public class CartridgeTests
    {
        [Test]
        public void LoadFileTest()
        {
            var cartridge = new Cartridge("c1j30.nes");
            var emulator = new Emulator();
            var program = emulator.Cpu.Disassemble(cartridge.PrgRom);
            Assert.IsTrue(true);
        }

        [Test]
        public void LoadIncorrectFileTest()
        {
            Assert.Throws<FormatException>(() =>
            {
                new Cartridge("invalid.nes");
            });
        }
    }
}
