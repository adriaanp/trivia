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

        List<string> players = new List<string>();

        protected int[] places = new int[6];
        int[] purses = new int[6];

        protected bool[] inPenaltyBox = new bool[6];

        int currentPlayer = 0;
        protected bool isGettingOutOfPenaltyBox;

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(String playerName)
        {


            players.Add(playerName);
            places[howManyPlayers()] = 0;
            purses[howManyPlayers()] = 0;
            inPenaltyBox[howManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return players.Count;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (!IsCurrentPlayerInPenaltyBox(roll))
            {
                MoveCurrentPlayer(roll);
                AskCurrentPlayerCategoryQuestion();
            }

        }

        private void AskCurrentPlayerCategoryQuestion()
        {
            Console.WriteLine(players[currentPlayer]
                    + "'s new location is "
                    + places[currentPlayer]);
            Console.WriteLine("The category is " + GetCurrentPlayerCategory());
            askQuestion();
        }

        private void MoveCurrentPlayer(int roll)
        {
            places[currentPlayer] = places[currentPlayer] + roll;
            if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;
        }

        private bool IsCurrentPlayerInPenaltyBox(int roll)
        {
            if (!inPenaltyBox[currentPlayer]) return false;

            return CanCurrentPlayerGetOutOfPenaltyBox(roll);
        }

        private bool CanCurrentPlayerGetOutOfPenaltyBox(int roll)
        {
            if (roll % 2 != 0)
            {
                isGettingOutOfPenaltyBox = true;
                Console.WriteLine(players[currentPlayer] + " is getting out of the penalty box");
                return false;
            }
            else
            {
                Console.WriteLine(players[currentPlayer] + " is not getting out of the penalty box");
                isGettingOutOfPenaltyBox = false;
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
            var currentTile = _board.Tiles.ElementAt(places[currentPlayer]);
            return currentTile.Category;
        }

        public bool wasCorrectlyAnswered()
        {
            if (inPenaltyBox[currentPlayer])
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    purses[currentPlayer]++;
                    Console.WriteLine(players[currentPlayer]
                            + " now has "
                            + purses[currentPlayer]
                            + " Gold Coins.");

                    bool winner = didPlayerWin();
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;

                    return winner;
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                purses[currentPlayer]++;
                Console.WriteLine(players[currentPlayer]
                        + " now has "
                        + purses[currentPlayer]
                        + " Gold Coins.");

                bool winner = didPlayerWin();
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;

                return winner;
            }
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }


        private bool didPlayerWin()
        {
            return !(purses[currentPlayer] == 6);
        }
    }

}
