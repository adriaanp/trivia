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
        //TODO: move questions to game board or need to find different way of question categories
        protected readonly Questions _questions;
        //TODO: Break dependency
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

        public bool IsPlayable()
        {
            return (HowManyPlayers() >= 2);
        }

        public bool AddPlayer(string playerName)
        {
            _players.Add(new Player(playerName, _board.StartingTile));

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
            return true;
        }

        public Player CurrentPlayer { get { return _players[currentPlayerMarker]; } }
        public IEnumerable<Player> Players { get { return _players; } }
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
            Console.WriteLine("The category is " + CurrentPlayer.CurrentTile.Category);
            AskQuestion();
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
                //TODO: flag not really necessary
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

        private void AskQuestion()
        {
            var nextQuestion = _questions.GetNextCategoryQuestion(CurrentPlayer.CurrentTile.Category);
            Console.WriteLine(nextQuestion);
        }

        public bool QuestionAnsweredCorrectly()
        {
            if (CurrentPlayer.IsInPenaltyBox)
            {
                if (CurrentPlayer.IsGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    GiveCurrentPlayerReward();

                    bool winner = DidPlayerWin();
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
                GiveCurrentPlayerReward();

                bool winner = DidPlayerWin();
                NextPlayerTurn();

                return winner;
            }
        }

        private void GiveCurrentPlayerReward()
        {
            CurrentPlayer.AddCoins(1);
            Console.WriteLine(CurrentPlayer.Name
                              + " now has "
                              + CurrentPlayer.Coins
                              + " Gold Coins.");
        }

        private void NextPlayerTurn()
        {
            currentPlayerMarker++;
            if (currentPlayerMarker == _players.Count) currentPlayerMarker = 0;
        }

        public bool QuestionAnsweredIncorrectly()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(CurrentPlayer.Name + " was sent to the penalty box");
            CurrentPlayer.IsInPenaltyBox = true;

            NextPlayerTurn();
            return true;
        }

        private bool DidPlayerWin()
        {
            return CurrentPlayer.Coins != 6;
        }
    }

}
