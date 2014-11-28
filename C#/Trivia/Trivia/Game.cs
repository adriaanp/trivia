using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trivia;

namespace UglyTrivia
{
    public class Game
    {

        private readonly IQuestionProvider _questionProvider;
        protected readonly Questions _questions;
        private readonly GameBoard _board;
        private readonly List<Player> _players = new List<Player>();

        public Game()
            : this(new QuestionProvider())
        {
        }

        public Game(IQuestionProvider questionProvider)
        {
            _questionProvider = questionProvider;
            _questions = questionProvider.GetQuestionsForGame();
            _board = new GameBoard();
        }

        int currentPlayerMarker = 0;

        public bool isPlayable()
        {
            return (HowManyPlayers() >= 2);
        }

        public bool AddPlayer(String playerName)
        {
            _players.Add(new Player(playerName, _board.StartingTile));

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
            return true;
        }

        public Player CurrentPlayer { get { return _players[currentPlayerMarker]; } }

        public IEnumerable<Player> Players
        {
            get { return _players; }
        }

        public GameBoard Board { get { return _board; } }

        public int HowManyPlayers()
        {
            return _players.Count;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(CurrentPlayer.Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (!IsCurrentPlayerInPenaltyBox(roll))
            {
                MoveCurrentPlayer(roll);
                AskCurrentPlayerCategoryQuestion();
            }

        }

        private void AskCurrentPlayerCategoryQuestion()
        {
            Console.WriteLine(CurrentPlayer.Name
                    + "'s new location is "
                    + (CurrentPlayer.CurrentTile.Position - 1));
            Console.WriteLine("The category is " + GetCurrentPlayerCategory());
            askQuestion();
        }

        private void MoveCurrentPlayer(int roll)
        {
            CurrentPlayer.CurrentTile = _board.GetNextTile(CurrentPlayer.CurrentTile, roll);
        }

        private bool IsCurrentPlayerInPenaltyBox(int roll)
        {
            if (!CurrentPlayer.IsInPenaltyBox) return false;

            return CanCurrentPlayerGetOutOfPenaltyBox(roll);
        }

        private bool CanCurrentPlayerGetOutOfPenaltyBox(int roll)
        {
            if (roll % 2 != 0)
            {
                CurrentPlayer.IsGettingOutOfPenaltyBox = true;
                Console.WriteLine(CurrentPlayer.Name + " is getting out of the penalty box");
                return false;
            }
            else
            {
                Console.WriteLine(CurrentPlayer.Name + " is not getting out of the penalty box");
                CurrentPlayer.IsGettingOutOfPenaltyBox = false;
                return true;
            }
        }

        private void askQuestion()
        {
            var nextQuestion = _questions.GetNextCategoryQuestion(GetCurrentPlayerCategory());
            Console.WriteLine(nextQuestion);
        }


        protected String GetCurrentPlayerCategory()
        {
            return CurrentPlayer.CurrentTile.Category;
        }

        public bool wasCorrectlyAnswered()
        {
            if (CurrentPlayer.IsInPenaltyBox)
            {
                if (CurrentPlayer.IsGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    CurrentPlayer.AddCoins(1);
                    Console.WriteLine(CurrentPlayer.Name
                            + " now has "
                            + CurrentPlayer.Coins
                            + " Gold Coins.");

                    bool winner = didPlayerWin();
                    NextPlayerTurn();

                    return winner;
                }
                else
                {
                    NextPlayerTurn();
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Answer was corrent!!!!");
                CurrentPlayer.AddCoins(1);
                Console.WriteLine(CurrentPlayer.Name
                        + " now has "
                        + CurrentPlayer.Coins
                        + " Gold Coins.");

                bool winner = didPlayerWin();
                NextPlayerTurn();

                return winner;
            }
        }

        private void NextPlayerTurn()
        {
            currentPlayerMarker++;
            if (currentPlayerMarker == _players.Count) currentPlayerMarker = 0;
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(CurrentPlayer.Name + " was sent to the penalty box");
            CurrentPlayer.IsInPenaltyBox = true;

            NextPlayerTurn();
            return true;
        }

        private bool didPlayerWin()
        {
            return CurrentPlayer.Coins != 6;
        }
    }

}
