using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Trivia.Tests
{
    [TestFixture]
    public class GoldenMaster
    {

        [Test]
        public void GoldenMasterTest()
        {
            using (var writer = new StreamWriter(@"test.txt"))
            {
                Console.SetOut(writer);
                for (int i = 0; i < 1000; i++)
                {
                    GameRunner.Main(new[] { i.ToString() });
                }
            }

            var original = File.OpenText(@"..\..\Tests\goldenmaster.txt").ReadToEnd();
            var test = File.OpenText("test.txt").ReadToEnd();

            Assert.AreEqual(original, test);
        }
    }
}
