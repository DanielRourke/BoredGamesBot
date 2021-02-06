using BoredGamesBot.Games.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoredGamesBot.Games.TicTacToe
{
    public class TicTacToeGame : Game
    {
        TicTacToeBoard board;
        int currentPlayer;
        List<Player> players;
        TicTacToeGame()
        {
            currentPlayer = 0;
        }

        public void Begin()
        {
            throw new NotImplementedException();
        }

        public void Conclude()
        {
            throw new NotImplementedException();
        }

        public void isGameOver()
        {
            throw new NotImplementedException();
        }

        public void NextTurn()
        {
            //print board
            //notify player its their turn
            throw new NotImplementedException();
        }

        public bool AttemptMove(Move move, ulong playerID)
        {
            //if move Valid
            if(board.ValidMove(move) && players[currentPlayer].ID == playerID)

            board.UpdateBoard(move);

        }

        public void SetUp()
        {
            throw new NotImplementedException();
        }
    }
}
