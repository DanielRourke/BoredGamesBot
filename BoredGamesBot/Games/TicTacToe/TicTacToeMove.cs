using BoredGamesBot.Games.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoredGamesBot.Games.TicTacToe
{
    class TicTacToeMove : Move
    {
        char col;
        int row;
        public TicTacToeMove(int r, char c )
        {
            row = r;
            col = c;
        }
    }
}
