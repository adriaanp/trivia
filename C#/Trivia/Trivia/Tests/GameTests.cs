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
        private GameForTesting _game;
        private StringWriter _writer;

        [SetUp]
        public void Setup()
        {
            _writer = new StringWriter();
            Console.SetOut(_writer);
            SetupGame();
        }

        [TearDown]
        public void TearDown()
        {
            _writer.Dispose();
        }

        private string ConsoleOutput()
        {
            return _writer.GetStringBuilder().ToString();
        }

        private void SetupGame()
        {
            _game = new GameForTesting();
            _game.add("Adriaan");
            _game.add("Player 2");
        }

        [Test]
        public void Add_Name_ShouldIncreasePlayerCount()
        {
            var game = new Game();
            game.add("Adriaan");
            Assert.That(game.howManyPlayers(), Is.EqualTo(1));
            game.add("Player 2");
            Assert.That(game.howManyPlayers(), Is.EqualTo(2));
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
            _game.PutPlayerInPenaltyBox(0);
            _game.roll(2);

            Assert.That(_game.IsGettingOutOfPenaltyBox, Is.False);
        }

        [Test]
        public void Roll_InPenaltyBoxAndNotEvenNumber_ShouldGetOutOfPenaltyBox()
        {
            _game.PutPlayerInPenaltyBox(0);
            _game.roll(3);

            Assert.That(_game.IsGettingOutOfPenaltyBox, Is.True);
        }

        [Test]
        public void Roll_NotInPenaltyBox_ShouldAskQuestion()
        {
            _game.roll(3);

            Assert.That(ConsoleOutput(), Is.StringContaining("Question 0"));
        }

        [Test]
        public void Roll_InPenaltyBoxAndGettingOut_ShouldAskQuestion()
        {
            _game.PutPlayerInPenaltyBox(0);
            _game.roll(3);

            Assert.That(ConsoleOutput(), Is.StringContaining("Question 0"));
        }

        [Test]
        public void Roll_NotInPenaltyBox_ShouldMovePlaces()
        {
            _game.roll(3);

            Assert.That(_game.PlayerPlace(0), Is.EqualTo(3));
        }

        [Test]
        public void Roll_InPenaltyBoxAndGettingOut_ShouldMovePlaces()
        {
            _game.PutPlayerInPenaltyBox(0);
            _game.roll(3);

            Assert.That(ConsoleOutput(), Is.StringContaining("Question 0"));
        }

        [Test]
        public void Roll_NotInPenaltyBoxPassEndOfBoard_ShouldMoveBack12Spaces()
        {
            _game.SetPlayerPlace(0, 10);
            _game.roll(5);

            Assert.That(_game.PlayerPlace(0), Is.EqualTo(3));
        }

        [Test]
        public void Roll_InPenaltyBoxAndGettingOutAndEndOfBoard_ShouldMoveBack12Spaces()
        {
            _game.SetPlayerPlace(0, 10);
            _game.PutPlayerInPenaltyBox(0);
            _game.roll(5);

            Assert.That(_game.PlayerPlace(0), Is.EqualTo(3));
        }
    }
}
