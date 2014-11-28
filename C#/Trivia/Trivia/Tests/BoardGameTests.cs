using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;
using NUnit.Framework;

namespace Trivia.Tests
{
    [TestFixture]
    public class BoardGameTests
    {

        [Test]
        public void New_ShouldCreateDefaultTilesForPop([Values(0,4,8)] int position)
        {
            var board = new GameBoard();
            Assert.That(board.Tiles.ElementAt(position).Category, Is.EqualTo("Pop"));
        }

        [Test]
        public void New_ShouldCreateDefaultTilesForScience([Values(1, 5, 9)] int position)
        {
            var board = new GameBoard();
            Assert.That(board.Tiles.ElementAt(position).Category, Is.EqualTo("Science"));
        }

        [Test]
        public void New_ShouldCreateDefaultTilesForSports([Values(2, 6, 10)] int position)
        {
            var board = new GameBoard();
            Assert.That(board.Tiles.ElementAt(position).Category, Is.EqualTo("Sports"));
        }

        [Test]
        public void New_ShouldCreateDefaultTilesForRock([Values(3, 7, 11)] int position)
        {
            var board = new GameBoard();
            Assert.That(board.Tiles.ElementAt(position).Category, Is.EqualTo("Rock"));
        }

        [Test]
        public void GetNextTile_Move_ShouldGetNextPositionOnBoard()
        {
            var board = new GameBoard();
            var currentTile = board.Tiles.ElementAt(0);

            var nextTile = board.GetNextTile(currentTile, 4);

            Assert.That(nextTile, Is.EqualTo(board.Tiles.ElementAt(4)));
        }

        [Test, Sequential]
        public void GetNextTile_MovePassedEndOfTiles_ShouldReturnFromStartAgain([Values(10, 5)] int start, [Values(5, 10)] int roll, [Values(3, 3)] int end)
        {
            var board = new GameBoard();
            var currentTile = board.Tiles.ElementAt(start);

            var nextTile = board.GetNextTile(currentTile, roll);

            Assert.That(nextTile, Is.EqualTo(board.Tiles.ElementAt(end)));
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void GetNextTile_TileNotOnBoard_ShouldThrowException()
        {
            var board = new GameBoard();

            var nextTile = board.GetNextTile(new Tile(1, "Test"), 5);
        }

        [Test]
        public void StartingTile_ShouldReturnFirstTile()
        {
            var board = new GameBoard();

            var startTile = board.StartingTile;

            Assert.That(startTile, Is.EqualTo(board.Tiles.ElementAt(0)));
        }
    }
}
