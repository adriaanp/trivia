namespace Trivia
{
    public class QuestionProvider : IQuestionProvider
    {
        public Questions GetQuestionsForGame()
        {
            var questions = new Questions();

            for (int i = 0; i < 50; i++)
            {
                questions.AddCategoryQuestions("Pop", new []{"Pop Question " + i});
                questions.AddCategoryQuestions("Science", new[] { "Science Question " + i });
                questions.AddCategoryQuestions("Sports", new[] { "Sports Question " + i });
                questions.AddCategoryQuestions("Rock", new[] { "Rock Question " + i });
            }
            return questions;
        }
    }
}