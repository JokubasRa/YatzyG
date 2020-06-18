using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yatzy;

namespace YatzyTest
{
    [TestClass]
    public class DiceTest
    {
        [TestMethod]
        public void TestDiceConstructor()
        {
            Dice dice;
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => dice = new Dice(-1));
        }
        [TestMethod]
        public void TestDiceNumValue()
        {
            for (int i = 1; i <= 5; i++)
            {
                Dice dice = new Dice(i);
                bool p = true;
                for (int j = 0; j < 100; j++)
                {
                    dice.Roll();
                    p &= dice.Num <= 6 && dice.Num >= 1;
                }
                Assert.AreEqual(true, p);
            }
        }
    }
}
