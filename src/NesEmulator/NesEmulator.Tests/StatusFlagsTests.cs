using NesEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesEmulator.Tests
{
    public class StatusFlagsTests
    {
        [Test]
        public void SetNFlagTest()
        {
            var flags = new StatusFlags(0b0010_0011)
            {
                N = 1
            };
            Assert.That(flags.Flags, Is.EqualTo(0b1010_0011));
        }

        [Test]
        public void ClearNFlagTest()
        {
            var flags = new StatusFlags(0b1010_0011)
            {
                N = 0
            };
            Assert.That(flags.Flags, Is.EqualTo(0b0010_0011));
        }

        [Test]
        public void GetNFlagTest()
        {
            var flags = new StatusFlags(0b1000_0000);
            Assert.That(flags.N, Is.EqualTo((Bit)1));
        }

        [Test]
        public void SetVFlagTest()
        {
            var flags = new StatusFlags(0b0010_0011)
            {
                V = 1
            };
            Assert.That(flags.Flags, Is.EqualTo(0b0110_0011));
        }

        [Test]
        public void ClearVFlagTest()
        {
            var flags = new StatusFlags(0b1110_0011)
            {
                V = 0
            };
            Assert.That(flags.Flags, Is.EqualTo(0b1010_0011));
        }

        [Test]
        public void GetVFlagTest()
        {
            var flags = new StatusFlags(0b0100_0000);
            Assert.That(flags.V, Is.EqualTo((Bit)1));
        }

        [Test]
        public void SetDFlagTest()
        {
            var flags = new StatusFlags(0b0010_0011)
            {
                D = 1
            };
            Assert.That(flags.Flags, Is.EqualTo(0b0010_1011));
        }

        [Test]
        public void ClearDFlagTest()
        {
            var flags = new StatusFlags(0b0010_1011)
            {
                D = 0
            };
            Assert.That(flags.Flags, Is.EqualTo(0b0010_0011));
        }

        [Test]
        public void GetDFlagTest()
        {
            var flags = new StatusFlags(0b0010_1011);
            Assert.That(flags.D, Is.EqualTo((Bit)1));
        }

        [Test]
        public void SetIFlagTest()
        {
            var flags = new StatusFlags(0b0010_0011)
            {
                I = 1
            };
            Assert.That(flags.Flags, Is.EqualTo(0b0010_0111));
        }

        [Test]
        public void ClearIFlagTest()
        {
            var flags = new StatusFlags(0b0010_0111)
            {
                I = 0
            };
            Assert.That(flags.Flags, Is.EqualTo(0b0010_0011));
        }

        [Test]
        public void GetIFlagTest()
        {
            var flags = new StatusFlags(0b0010_0111);
            Assert.That(flags.I, Is.EqualTo((Bit)1));
        }

        [Test]
        public void SetZFlagTest()
        {
            var flags = new StatusFlags(0b0010_0001)
            {
                Z = 1
            };
            Assert.That(flags.Flags, Is.EqualTo(0b0010_0011));
        }

        [Test]
        public void ClearZFlagTest()
        {
            var flags = new StatusFlags(0b0010_0011)
            {
                Z = 0
            };
            Assert.That(flags.Flags, Is.EqualTo(0b0010_0001));
        }

        [Test]
        public void GetZFlagTest()
        {
            var flags = new StatusFlags(0b0010_0111);
            Assert.That(flags.Z, Is.EqualTo((Bit)1));
        }

        [Test]
        public void SetCFlagTest()
        {
            var flags = new StatusFlags(0b0010_0000)
            {
                C = 1
            };
            Assert.That(flags.Flags, Is.EqualTo(0b0010_0001));
        }

        [Test]
        public void ClearCFlagTest()
        {
            var flags = new StatusFlags(0b0010_0001)
            {
                C = 0
            };
            Assert.That(flags.Flags, Is.EqualTo(0b0010_0000));
        }

        [Test]
        public void GetCFlagTest()
        {
            var flags = new StatusFlags(0b0010_0001);
            Assert.That(flags.C, Is.EqualTo((Bit)1));
        }

        [Test]
        public void SetBit5FlagTest()
        {
            var flags = new StatusFlags(0b0000_0000)
            {
                Bit5 = 1
            };
            Assert.That(flags.Flags, Is.EqualTo(0b0010_0000));
        }

        [Test]
        public void ClearBit5FlagTest()
        {
            var flags = new StatusFlags(0b0010_0001)
            {
                Bit5 = 0
            };
            Assert.That(flags.Flags, Is.EqualTo(0b0000_0001));
        }

        [Test]
        public void GetBit5FlagTest()
        {
            var flags = new StatusFlags(0b0010_0001);
            Assert.That(flags.Bit5, Is.EqualTo((Bit)1));
        }

        [Test]
        public void SetBit4FlagTest()
        {
            var flags = new StatusFlags(0b0000_0000)
            {
                Bit4 = 1
            };
            Assert.That(flags.Flags, Is.EqualTo(0b0001_0000));
        }

        [Test]
        public void ClearBit4FlagTest()
        {
            var flags = new StatusFlags(0b0011_0001)
            {
                Bit4 = 0
            };
            Assert.That(flags.Flags, Is.EqualTo(0b0010_0001));
        }

        [Test]
        public void GetBit4FlagTest()
        {
            var flags = new StatusFlags(0b0011_0001);
            Assert.That(flags.Bit4, Is.EqualTo((Bit)1));
        }
    }
}
