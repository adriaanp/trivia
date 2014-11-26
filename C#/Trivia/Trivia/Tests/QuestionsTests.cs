using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Trivia.Tests
{
    [TestFixture]
    public class QuestionsTests
    {
        [Test]
        public void AddCategoryQuestions_WithQuestions_ShouldContainCategoryAndQuestions()
        {
            var questions = new Questions();
            questions.AddCategoryQuestions("Pop", new string[] {"Pop Question 1", "Pop Question 2"});
            Assert.That(questions["Pop"].Count, Is.EqualTo(2));
        }

        [Test]
        public void AddCategoryQuestions_TwiceWithSameCategory_ShouldCombinedQuestions()
        {
            var questions = new Questions();
            questions.AddCategoryQuestions("Pop", new []{"Pop Question 1"});
            questions.AddCategoryQuestions("Pop", new[] { "Pop Question 2" });

            Assert.That(questions["Pop"].Count, Is.EqualTo(2));
        }

        [Test]
        public void GetNextCategoryQuestion_ValidCategory_ShouldReturnFirstQuestionInList()
        {
            var questions = new Questions();
            questions.AddCategoryQuestions("Pop", new[]{"Pop Question 1", "Pop Question 2"});

            var question = questions.GetNextCategoryQuestion("Pop");

            Assert.That(question, Is.EqualTo("Pop Question 1"));
        }

        [Test, ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetNextCategoryQuestion_InvalidCategory_ShouldThrowException()
        {
            var questions = new Questions();

            var question = questions.GetNextCategoryQuestion("Pop");
        }

        [Test]
        public void GetNextCategoryQuestion_ShouldReduceNumberOfQuestionsInCategory()
        {
            // should this be responsiblity of questions?
            var questions = new Questions();
            questions.AddCategoryQuestions("Pop", new[] { "Pop Question 1", "Pop Question 2" });

            var question = questions.GetNextCategoryQuestion("Pop");

            Assert.That(questions["Pop"].Count, Is.EqualTo(1));
        }

        //questions.Reset();
        //questions.AddCategoryQuestions("Pop", Questions); create 50 questions?
        //var q = questions.GetNextAndRemoveQuestionFromCategory("Pop");

        //q.Discard();

        //Assert.That(questions.Count("Pop"), Is.EqualTo(49));
    }
}
