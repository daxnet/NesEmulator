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
            var cartridge = new Cartridge("bm.nes");
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
