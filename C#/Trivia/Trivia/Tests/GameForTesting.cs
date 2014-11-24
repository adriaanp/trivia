﻿using System;
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

        public IList<string> PopQuestions
        {
            get { return popQuestions.ToList(); }
        }

        public IList<string> ScienceQuestions
        {
            get { return scienceQuestions.ToList(); }
        }

        public IList<string> SportQuestions
        {
            get { return sportsQuestions.ToList(); }
        }

        public IList<string> RockQuestions
        {
            get { return rockQuestions.ToList(); }
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
