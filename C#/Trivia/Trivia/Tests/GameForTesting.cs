using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UglyTrivia;

namespace Trivia.Tests
{
    public class GameForTesting: Game
    {
        public void PutPlayerInPenaltyBox(int player)
        {
            inPenaltyBox[player] = true;
        }

        public void RemovePlayerFromPenaltyBox(int player)
        {
            inPenaltyBox[player] = false;
        }
    }
}
