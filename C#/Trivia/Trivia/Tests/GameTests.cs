using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UglyTrivia;

namespace Trivia.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void Add_Name_ShouldIncreasePlayerCount()
        {
            var game = new Game();
            game.add("Adriaan");
            Assert.AreEqual(1, game.howManyPlayers());
            game.add("Player 2");
            Assert.AreEqual(2, game.howManyPlayers());
        }

        // if in penalty box
            // if not even roll
                // get out of penalty box
                // askQuestion
            // if even roll
                // not getting out penalty box
        // if not in penalty box
            // move and check location
            // askQuestion
        [Test]
        public void Roll_InPenaltyBoxAndEvenNumber_ShouldNotGetOutOfPenaltyBox()
        {
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);
                var game = new Game();
                game.add("Adriaan");
                game.add("Player 2");

                game.wrongAnswer();
                game.wrongAnswer();

                game.roll(2);

                var output = writer.GetStringBuilder().ToString();
                Assert.IsTrue(output.Contains("Adriaan is not getting out of the penalty box"));
            }
        }

        [Test]
        public void Roll_InPenaltyBoxAndEvenNumber_ShouldNotGetOutOfPenaltyBox2()
        {
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);
                var game = new GameForTesting();
                game.add("Adriaan");
                game.add("Player 2");

                game.PutPlayerInPenaltyBox(0);
                game.roll(2);

                var output = writer.GetStringBuilder().ToString();
                Assert.IsTrue(output.Contains("Adriaan is not getting out of the penalty box"));
            }
        }
    
    }
}
