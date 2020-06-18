using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yatzy;

namespace YatzyTest
{
    [TestClass]
    public class PlayerTest
    {

        Player player = new Player(1);


        [TestMethod]
        public void TestPlayerConstructor()
        {
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => new Player(-1));
        }
        
        [TestMethod]

        public void TestParseInputRerollNull()
        {
            Assert.ThrowsException<System.NullReferenceException>(() => player.ParseInputReroll(null));
        }

        [TestMethod]
        public void TestParseInputRerollZeroInput()
        {
            Assert.IsTrue(player.ParseInputReroll("0"));
        }

        [TestMethod]
        public void TestParseInputRerollNegativeInput()
        {         
            Assert.IsFalse(player.ParseInputReroll("-5 2 1"));
        }

        [TestMethod]
        public void TestParseInputRerollNumberOutOfRange()
        {           
            Assert.IsFalse(player.ParseInputReroll("6 1 2"));
        }

        [TestMethod]
        public void TestParseInputRerollAlphaInput()
        {
            Assert.IsFalse(player.ParseInputReroll("abc"));
        }

        [TestMethod]
        public void TestParseInputRerollCorrectInput1()
        {
            Assert.IsTrue(player.ParseInputReroll("1"));
        }

        [TestMethod]
        public void TestParseInputRerollCorrectInput2()
        {
            Assert.IsTrue(player.ParseInputReroll("5"));
        }

        [TestMethod]
        public void TestParseInputRerollCorrectInput3()
        {
            Assert.IsTrue(player.ParseInputReroll("1 2 3 4 5"));
        }

        [TestMethod]
        public void TestParseInputScoringNull()
        {
            Assert.ThrowsException<System.NullReferenceException>(() => player.ParseInputScoring(null));
        }

        [TestMethod]
        public void TestParseInputScoringEmpty()
        {
            Assert.IsFalse(player.ParseInputScoring(""));
        }

        [TestMethod]
        public void TestParseInputScoringCorrectInput()
        {
            Assert.IsTrue(player.ParseInputScoring("FIVES"));
        }

    }
}
