using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Trivia.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void New_ShouldSetNameAndTile()
        {
            var name = "Adriaan";
            var startingTile = new Tile(1, "Test");

            var player = new Player(name, startingTile);

            Assert.That(player.Name, Is.EqualTo(name));
            Assert.That(player.CurrentTile, Is.EqualTo(startingTile));
        }

        [Test]
        public void AddCoins_ShouldAddCoinsToTotal()
        {
            var player = new Player("Adriaan", new Tile(1, ""));

            player.AddCoins(5);

            Assert.That(player.Coins, Is.EqualTo(5));

            player.AddCoins(4);

            Assert.That(player.Coins, Is.EqualTo(9));
        }
    }
}
