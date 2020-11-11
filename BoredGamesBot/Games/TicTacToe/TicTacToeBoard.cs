using BoredGamesBot.Games.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoredGamesBot.Games.TicTacToe
{
    class TicTacToeBoard : Board
    {

        public TicTacToeBoard(int h = 3, int w =3): base(h,w)
        {
            SetBoardState(88);
        }

        //public override string ConvertToSymbol(int s)
        //{
        //    if (s == -1)
        //        return "\u2004" + "\u2009" + "\u2009" + "\u200A" ;
        //    else if (s == 2)
        //        return "X";
        //    else
        //        return s.ToString();
        //}

        public override void UpdateBoard(Move move)
        {
            throw new NotImplementedException();
        }

        public override bool ValidMove(Move move)
        {
            throw new NotImplementedException();
        }

        public override List<Move> GetPossibleMoves()
        {
            throw new NotImplementedException();
        }
    }
}
