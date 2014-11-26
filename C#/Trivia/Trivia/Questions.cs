using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Questions
    {
        private IDictionary<string, IList<string>> _categories = new Dictionary<string, IList<string>>();

        public void AddCategoryQuestions(string category, IEnumerable<string> questions)
        {
            if (!_categories.ContainsKey(category)) _categories.Add(category, new List<string>());
            questions.ForEach(question => _categories[category].Add(question));
        }

        public IList<string> this[string category]
        {
            get { return _categories[category]; }
        }

        public string GetNextCategoryQuestion(string category)
        {
            if (!_categories.ContainsKey(category))
                throw new IndexOutOfRangeException("No Category Exists");

            var questions = this[category];
            var currentQuestion = questions[0];
            questions.RemoveAt(0);
            return currentQuestion;
        }
    }
}