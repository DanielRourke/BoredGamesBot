using BoredGamesBot.Games.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoredGamesBot.Games.TicTacToe
{
    class TicTacToeMove : Move
    {
        public char Col { get; set; }
        public int Row { get; set; }

        public override bool AttemptInit(string s)
        {

            if (s.Length != 3 && s[0] != 'A' && s[0] != 'B' && s[0] != 'C' &&
                                s[2] != '1' && s[2] != '2' && s[2] != '3')
            {
                return false;
            }

            Cost = 1;
            Col = s[0];
            Row = s[2] - '0';
            return true;
        }
    }
}
