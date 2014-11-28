using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using UglyTrivia;

namespace Trivia.Tests
{
    [TestFixture]
    public class GameTests
    {
        private Game _game;
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
            _game = new Game();
            _game.AddPlayer("Adriaan");
            _game.AddPlayer("Player 2");
        }

        [Test]
        public void Add_Name_ShouldIncreasePlayerCount()
        {
            var game = new Game();
            game.AddPlayer("Adriaan");
            Assert.That(game.HowManyPlayers(), Is.EqualTo(1));
            game.AddPlayer("Player 2");
            Assert.That(game.HowManyPlayers(), Is.EqualTo(2));
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
                game.AddPlayer("Adriaan");
                game.AddPlayer("Player 2");

                game.QuestionAnsweredIncorrectly();
                game.QuestionAnsweredIncorrectly();

                game.Roll(2);

                var output = writer.GetStringBuilder().ToString();
                Assert.IsTrue(output.Contains("Adriaan is not getting out of the penalty box"));
            }
        }

        [Test]
        public void Roll_InPenaltyBoxAndEvenNumber_ShouldNotGetOutOfPenaltyBox2()
        {
            _game.CurrentPlayer.IsInPenaltyBox = true;
            _game.Roll(2);

            Assert.That(_game.CurrentPlayer.IsGettingOutOfPenaltyBox, Is.False);
        }

        [Test]
        public void Roll_InPenaltyBoxAndNotEvenNumber_ShouldGetOutOfPenaltyBox()
        {
            _game.CurrentPlayer.IsInPenaltyBox = true;
            _game.Roll(3);

            Assert.That(_game.CurrentPlayer.IsGettingOutOfPenaltyBox, Is.True);
        }

        [Test]
        public void Roll_NotInPenaltyBox_ShouldAskQuestion()
        {
            _game.Roll(3);

            Assert.That(ConsoleOutput(), Is.StringContaining("Question 0"));
        }

        [Test]
        public void Roll_InPenaltyBoxAndGettingOut_ShouldAskQuestion()
        {
            _game.CurrentPlayer.IsInPenaltyBox = true;
            _game.Roll(3);

            Assert.That(ConsoleOutput(), Is.StringContaining("Question 0"));
        }

        [Test]
        public void Roll_NotInPenaltyBox_ShouldMovePlaces()
        {
            _game.Roll(3);

            Assert.That(_game.CurrentPlayer.CurrentTile.Position, Is.EqualTo(4));
        }

        [Test]
        public void Roll_InPenaltyBoxAndGettingOut_ShouldMovePlaces()
        {
            _game.CurrentPlayer.IsInPenaltyBox = true;
            _game.Roll(3);

            Assert.That(ConsoleOutput(), Is.StringContaining("Question 0"));
        }

        [Test]
        public void AskQuestion_PopQuestion_ShouldPrintPopOutQuestion()
        {
            _game.CurrentPlayer.CurrentTile = _game.Board.Tiles.First();
            _game.Roll(0);
            
            Assert.That(ConsoleOutput(), Is.StringContaining("Pop Question 0"));
        }

        [Test]
        public void AskQuestion_ScienceQuestion_ShouldPrintOutScienceQuestion()
        {
            _game.CurrentPlayer.CurrentTile = _game.Board.Tiles.ElementAt(1);
            _game.Roll(0);

            Assert.That(ConsoleOutput(), Is.StringContaining("Science Question 0"));
        }

        [Test]
        public void AskQuestion_SportQuestion_ShouldPrintOutSportQuestion()
        {
            _game.CurrentPlayer.CurrentTile = _game.Board.Tiles.ElementAt(2);
            _game.Roll(0);

            Assert.That(ConsoleOutput(), Is.StringContaining("Sports Question 0"));
        }

        [Test]
        public void AskQuestion_RockQuestion_ShouldPrintOutRockQuestion()
        {
            _game.CurrentPlayer.CurrentTile = _game.Board.Tiles.ElementAt(3);
            _game.Roll(0);

            Assert.That(ConsoleOutput(), Is.StringContaining("Rock Question 0"));
        }

        [Test]
        public void NewGame_ShouldCallQuestionProviderGetQuestionsForGame()
        {
            var provider = MockRepository.GenerateMock<IQuestionProvider>();

            var game = new Game(provider);

            provider.AssertWasCalled(p => p.GetQuestionsForGame());
        }
    }
}
