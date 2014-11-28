namespace Trivia
{
    public class Tile
    {
        public Tile(int position, string category)
        {
            Position = position;
            Category = category;
        }
        public int Position { get; private set; }
        public string Category { get; private set; }
    }
}