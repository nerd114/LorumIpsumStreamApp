using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LorumIpsum;

namespace LorumIpsumStreamTests
{
    [TestClass]
    public class LorumIpsumTests
    {
        [TestMethod]
        public void TestCharacterCount()
        {
            LpStream lpStream = new LpStream();
            lpStream.ReadText(10);

            string text = lpStream.LastReadText;
            int characters = text.Length;

            Assert.AreEqual(characters, lpStream.Characters);
        }

        [TestMethod]
        public void TestWordCount()
        {
            LpStream lpStream = new LpStream();
            lpStream.ReadText(10);

            string text = lpStream.LastReadText;
            int words = text.Trim().Split(' ').Length;

            Assert.AreEqual(words, lpStream.Words);
        }

        [TestMethod]
        public void TestTotalCharactersNotZero()
        {
            LpStream lpStream = new LpStream();
            lpStream.ReadText(1);

            Assert.AreNotEqual(0, lpStream.Characters);
        }

        [TestMethod]
        public void TestTotalWordsNotZero()
        {
            LpStream lpStream = new LpStream();
            lpStream.ReadText(1);

            Assert.AreNotEqual(0, lpStream.Words);
        }

        [TestMethod]
        public void TestLargest()
        {
            LpStream lpStream = new LpStream();
            lpStream.ReadText(5);

            int count = lpStream.Largest5.Count;

            Assert.AreNotEqual(0, count);
        }

        [TestMethod]
        public void TestSmallest()
        {
            LpStream lpStream = new LpStream();
            lpStream.ReadText(5);

            int count = lpStream.Smallest5.Count;

            Assert.AreNotEqual(0, count);
        }

        [TestMethod]
        public void TestHasCharacters()
        {
            LpStream lpStream = new LpStream();
            lpStream.ReadText(1);

            int count = lpStream.AllCharacters.Count;

            Assert.AreNotEqual(0, count);
        }

        [TestMethod]
        public async Task TestCharacterCountAsync()
        {
            LpStream lpStream = new LpStream();
            await lpStream.ReadTextAsync(10);

            string text = lpStream.LastReadText;
            int characters = text.Length;

            Assert.AreEqual(characters, lpStream.Characters);
        }

        [TestMethod]
        public async Task TestDoubleSpaceAsync()
        {
            LpStream lpStream = new LpStream();
            await lpStream.ReadTextAsync(500);

            string text = lpStream.LastReadText;
            int doubleSpace = text.IndexOf("  ");

            Assert.AreEqual(-1, doubleSpace);
        }
    }
}
