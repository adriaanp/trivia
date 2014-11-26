using NUnit.Framework;

namespace Trivia.Tests
{
    [TestFixture]
    public class QuestionProviderTests
    {
        [Test]
        public void GetQuestionsForGame_ShouldCreateDefaultQuestionsForGame()
        {
            var provider = new QuestionProvider();

            var questions = provider.GetQuestionsForGame();

            new []{"Pop", "Science", "Sports", "Rock"}.ForEach(category =>
            {
                for (int i = 0; i < 50; i++)
                {
                    Assert.That(questions[category][i], Is.EqualTo(category + " Question " + i));
                }
            });
        }
    }
}