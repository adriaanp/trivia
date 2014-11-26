using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UglyTrivia;

namespace Trivia.Tests
{
    public class GameForTesting : Game
    {
        public void PutPlayerInPenaltyBox(int player)
        {
            inPenaltyBox[player] = true;
        }

        public void RemovePlayerFromPenaltyBox(int player)
        {
            inPenaltyBox[player] = false;
        }

        public bool IsGettingOutOfPenaltyBox
        {
            get {return isGettingOutOfPenaltyBox;}
        }

        //TODO: Remove, is covered in question tests
        public IList<string> PopQuestions
        {
            get { return _questions["Pop"].ToList(); }
        }

        public IList<string> ScienceQuestions
        {
            get { return _questions["Science"].ToList(); }
        }

        public IList<string> SportQuestions
        {
            get { return _questions["Sports"].ToList(); }
        }

        public IList<string> RockQuestions
        {
            get { return _questions["Rock"].ToList(); }
        }

        public int PlayerPlace(int p)
        {
            return places[p];
        }

        public void SetPlayerPlace(int player, int place)
        {
            places[player] = place;
        }

        public string CurrentPlayerCategory()
        {
            return currentCategory();
        }
    }
}
