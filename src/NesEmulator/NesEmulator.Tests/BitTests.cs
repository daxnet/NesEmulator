using NesEmulator.Core;

namespace NesEmulator.Tests
{
    public class BitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateBitIntTest()
        {
            var bit = new Bit(1);
            Assert.That(bit.Value, Is.EqualTo(1));
        }

        [Test]
        public void CreateBitBoolTest()
        {
            var bit = new Bit(true);
            Assert.That(bit.Value, Is.EqualTo(1));
        }

        [Test]
        public void BitAnd1Test()
        {
            Bit bit1 = new(1);
            Bit bit2 = new(0);
            Bit bit3 = bit1 & bit2;
            Assert.That(bit3.Value, Is.EqualTo(0));
        }

        [Test]
        public void BitAnd2Test()
        {
            Bit bit1 = new(1);
            Bit bit2 = new(1);
            Bit bit3 = bit1 & bit2;
            Assert.That(bit3.Value, Is.EqualTo(1));
        }

        [Test]
        public void BitAnd3Test()
        {
            Bit bit1 = new(0);
            Bit bit2 = new(0);
            Bit bit3 = bit1 & bit2;
            Assert.That(bit3.Value, Is.EqualTo(0));
        }

        [Test]
        public void BitOr1Test()
        {
            Bit bit1 = new(0);
            Bit bit2 = new(0);
            Bit bit3 = bit1 | bit2;
            Assert.That(bit3.Value, Is.EqualTo(0));
        }

        [Test]
        public void BitOr2Test()
        {
            Bit bit1 = new(1);
            Bit bit2 = new(0);
            Bit bit3 = bit1 | bit2;
            Assert.That(bit3.Value, Is.EqualTo(1));
        }

        [Test]
        public void BitOr3Test()
        {
            Bit bit1 = new(0);
            Bit bit2 = new(1);
            Bit bit3 = bit1 | bit2;
            Assert.That(bit3.Value, Is.EqualTo(1));
        }

        [Test]
        public void BitOr4Test()
        {
            Bit bit1 = new(1);
            Bit bit2 = new(1);
            Bit bit3 = bit1 | bit2;
            Assert.That(bit3.Value, Is.EqualTo(1));
        }

        [Test]
        public void Bitwise1Test()
        {
            Bit bit1 = new(1);
            Bit bit2 = ~bit1;
            Assert.That(bit2.Value, Is.EqualTo(0));
        }

        [Test]
        public void Bitwise2Test()
        {
            Bit bit1 = new(0);
            Bit bit2 = ~bit1;
            Assert.That(bit2.Value, Is.EqualTo(1));
        }

        [Test]
        public void BoolValue1Test()
        {
            Bit bit1 = new(0);
            Assert.That(bit1.BoolValue, Is.EqualTo(false));
        }

        [Test]
        public void BoolValue2Test()
        {
            Bit bit1 = new(1);
            Assert.That(bit1.BoolValue, Is.EqualTo(true));
        }

        [Test]
        public void GetIntValue1Test()
        {
            Bit bit1 = 1;
            Assert.That(bit1.BoolValue, Is.EqualTo(true));
        }

        [Test]
        public void GetIntValue2Test()
        {
            Bit bit1 = 0;
            Assert.That(bit1.BoolValue, Is.EqualTo(false));
        }

        [Test]
        public void ConvertInt1Test()
        {
            Bit bit1 = new(true);
            int val = bit1;
            Assert.That(val, Is.EqualTo(1));
        }

        [Test]
        public void ConvertInt2Test()
        {
            Bit bit1 = new(false);
            int val = bit1;
            Assert.That(val, Is.EqualTo(0));
        }

        [Test]
        public void Xor1Test()
        {
            Bit b1 = 1;
            Bit b2 = 1;
            Assert.That((b1 ^ b2).Value, Is.EqualTo(0));
        }

        [Test]
        public void Xor2Test()
        {
            Bit b1 = 1;
            Bit b2 = 0;
            Assert.That((b1 ^ b2).Value, Is.EqualTo(1));
        }

        [Test]
        public void Xor3Test()
        {
            Bit b1 = 0;
            Bit b2 = 1;
            Assert.That((b1 ^ b2).Value, Is.EqualTo(1));
        }

        [Test]
        public void Xor4Test()
        {
            Bit b1 = 0;
            Bit b2 = 0;
            Assert.That((b1 ^ b2).Value, Is.EqualTo(0));
        }

        [Test]
        public void EqualsTest()
        {
            Bit b1 = 1;
            Bit b2 = true;
            Assert.That(b1 == b2, Is.True);
        }

        [Test]
        public void NotEqualsTest()
        {
            Bit b1 = 1;
            Bit b2 = false;
            Assert.That(b1 != b2, Is.True);
        }

        [Test]
        public void SetHighestBitTest()
        {
            byte src = 0b0110_1000;
            byte dest = Bit.Set(src, 7);
            Assert.That(dest, Is.EqualTo(0b1110_1000));
        }

        [Test]
        public void SetLowestBitTest()
        {
            byte src = 0b0110_1000;
            byte dest = Bit.Set(src, 0);
            Assert.That(dest, Is.EqualTo(0b0110_1001));
        }

        [Test]
        public void ClearHighestBitTest()
        {
            byte src = 0b1100_1000;
            byte dest = Bit.Clear(src, 7);
            Assert.That(dest, Is.EqualTo(0b0100_1000));
        }

        [Test]
        public void ClearLowestBitTest()
        {
            byte src = 0b1100_1001;
            byte dest = Bit.Clear(src, 0);
            Assert.That(dest, Is.EqualTo(0b1100_1000));
        }

        [Test]
        public void SetBitTo1Test()
        {
            byte src = 0b0110_1000;
            byte dest = Bit.SetBit(src, 2, 1);
            Assert.That(dest, Is.EqualTo(0b0110_1100));
        }

        [Test]
        public void SetBitTo0Test()
        {
            byte src = 0b0110_1000;
            byte dest = Bit.SetBit(src, 3, 0);
            Assert.That(dest, Is.EqualTo(0b0110_0000));
        }

        [Test]
        public void GetBit1Test()
        {
            byte src = 0b0100_1010;
            var bit = Bit.GetBit(src, 6);
            Assert.That(bit, Is.EqualTo((Bit)1));
        }

        [Test]
        public void GetBit2Test()
        {
            byte src = 0b0100_1010;
            var bit = Bit.GetBit(src, 1);
            Assert.That(bit, Is.EqualTo((Bit)1));
        }

        [Test]
        public void GetBit3Test()
        {
            byte src = 0b0100_1010;
            var bit = Bit.GetBit(src, 2);
            Assert.That(bit, Is.EqualTo((Bit)0));
        }

        [Test]
        public void GetBit4Test()
        {
            byte src = 0b0100_1010;
            var bit = Bit.GetBit(src, 7);
            Assert.That(bit, Is.EqualTo((Bit)0));
        }
    }
}