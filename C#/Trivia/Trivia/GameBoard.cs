using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class GameBoard
    {
        private readonly List<Tile> _tiles;

        public IEnumerable<Tile> Tiles
        {
            get { return _tiles; }
        }

        public Tile StartingTile
        {
            get { return _tiles.First(); }
        }

        public GameBoard()
        {
            _tiles = new List<Tile>()
            {
                new Tile(1, "Pop"),
                new Tile(2, "Science"),
                new Tile(3, "Sports"),
                new Tile(4, "Rock"),
                new Tile(5, "Pop"),
                new Tile(6, "Science"),
                new Tile(7, "Sports"),
                new Tile(8, "Rock"),
                new Tile(9, "Pop"),
                new Tile(10, "Science"),
                new Tile(11, "Sports"),
                new Tile(12, "Rock")
            };
        }

        public Tile GetNextTile(Tile startTile, int spacesToMove)
        {
            var index = _tiles.IndexOf(startTile);
            if (index < 0) throw new ArgumentException("Tile not on board.");

            var nextTileIndex = index + spacesToMove;

            if (nextTileIndex > _tiles.Count - 1) nextTileIndex = nextTileIndex - _tiles.Count;
            return _tiles[nextTileIndex];
        }
    }
}
