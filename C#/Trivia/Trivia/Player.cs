using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivia
{
    public class Player
    {
        public string Name { get; private set; }
        public Tile CurrentTile { get; set; }
        public int Coins { get; private set; }
        public bool IsInPenaltyBox { get; set; }

        public Player(string name, Tile startingTile)
        {
            Name = name;
            CurrentTile = startingTile;
            Coins = 0;
        }

        public void AddCoins(int coins)
        {
            Coins += coins;
        }
    }
}
