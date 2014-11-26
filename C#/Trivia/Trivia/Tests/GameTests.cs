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

                game.Roll(2);

                var output = writer.GetStringBuilder().ToString();
                Assert.IsTrue(output.Contains("Adriaan is not getting out of the penalty box"));
            }
        }

        [Test]
        public void Roll_InPenaltyBoxAndEvenNumber_ShouldNotGetOutOfPenaltyBox2()
        {
            _game.PutPlayerInPenaltyBox(0);
            _game.Roll(2);

            Assert.That(_game.IsGettingOutOfPenaltyBox, Is.False);
        }

        [Test]
        public void Roll_InPenaltyBoxAndNotEvenNumber_ShouldGetOutOfPenaltyBox()
        {
            _game.PutPlayerInPenaltyBox(0);
            _game.Roll(3);

            Assert.That(_game.IsGettingOutOfPenaltyBox, Is.True);
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
            _game.PutPlayerInPenaltyBox(0);
            _game.Roll(3);

            Assert.That(ConsoleOutput(), Is.StringContaining("Question 0"));
        }

        [Test]
        public void Roll_NotInPenaltyBox_ShouldMovePlaces()
        {
            _game.Roll(3);

            Assert.That(_game.PlayerPlace(0), Is.EqualTo(3));
        }

        [Test]
        public void Roll_InPenaltyBoxAndGettingOut_ShouldMovePlaces()
        {
            _game.PutPlayerInPenaltyBox(0);
            _game.Roll(3);

            Assert.That(ConsoleOutput(), Is.StringContaining("Question 0"));
        }

        [Test]
        public void Roll_NotInPenaltyBoxPassEndOfBoard_ShouldMoveBack12Spaces()
        {
            _game.SetPlayerPlace(0, 10);
            _game.Roll(5);

            Assert.That(_game.PlayerPlace(0), Is.EqualTo(3));
        }

        [Test]
        public void Roll_InPenaltyBoxAndGettingOutAndEndOfBoard_ShouldMoveBack12Spaces()
        {
            _game.SetPlayerPlace(0, 10);
            _game.PutPlayerInPenaltyBox(0);
            _game.Roll(5);

            Assert.That(_game.PlayerPlace(0), Is.EqualTo(3));
        }

        // asking question, should reduce # q
        // 0,4,8 - pop q
        // 1,5,9 - science q
        // 2,6,10 - sport q
        // else rock questions

        [Test]
        public void Category_PopLocations_ShouldAskPopQuestions([Values(0, 4, 8)] int location)
        {
            _game.SetPlayerPlace(0, location);
            Assert.That(_game.CurrentPlayerCategory(), Is.EqualTo("Pop"));
        }

        [Test]
        public void Category_ScienceLocations_ShouldAskScienceQuestions([Values(1,5,9)] int location)
        {
            _game.SetPlayerPlace(0, location);
            Assert.That(_game.CurrentPlayerCategory(), Is.EqualTo("Science"));
        }

        [Test]
        public void Category_SportLocations_ShouldAskSportQuestions([Values(2,6,10)] int location)
        {
            _game.SetPlayerPlace(0, location);
            Assert.That(_game.CurrentPlayerCategory(), Is.EqualTo("Sports"));
        }

        [Test]
        public void Category_RockLocations_ShouldAskRockQuestions([Values(3, 7, 11)] int location)
        {
            _game.SetPlayerPlace(0, location);
            Assert.That(_game.CurrentPlayerCategory(), Is.EqualTo("Rock"));
        }

        [Test]
        public void AskQuestion_PopQuestion_ShouldPrintPopOutQuestion()
        {
            _game.SetPlayerPlace(0, 0);
            _game.Roll(0);
            
            Assert.That(ConsoleOutput(), Is.StringContaining("Pop Question 0"));
        }

        [Test]
        public void AskQuestion_ScienceQuestion_ShouldPrintOutScienceQuestion()
        {
            _game.SetPlayerPlace(0, 1);
            _game.Roll(0);

            Assert.That(ConsoleOutput(), Is.StringContaining("Science Question 0"));
        }

        [Test]
        public void AskQuestion_SportQuestion_ShouldPrintOutSportQuestion()
        {
            _game.SetPlayerPlace(0, 2);
            _game.Roll(0);

            Assert.That(ConsoleOutput(), Is.StringContaining("Sports Question 0"));
        }

        [Test]
        public void AskQuestion_RockQuestion_ShouldPrintOutRockQuestion()
        {
            _game.SetPlayerPlace(0, 3);
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
