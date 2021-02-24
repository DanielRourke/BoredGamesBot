using BoredGamesBot.Games.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoredGamesBot.Games.TicTacToe
{
    class TicTacToeBoard : Board<TicTacToeMove>
    {
        public TicTacToeBoard(int h = 3, int w =3): base(h,w)
        {
            SetBoardState(-1);
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

        public override void UpdateBoard(TicTacToeMove move)
        {
            int r = move.Row ;
            int c = move.Col - 'A';
            boardState[r,c] = move.Token;
        }

        public override bool ValidMove(TicTacToeMove move)
        {
            if (!(move.Row != null && move.Col != null))
                return false;

            int r = move.Row;
            int c = move.Col - 'A';
            if (r < 0 || r >= width || c < 0 || c >= height || boardState[r,c] != -1)
                return false;

            return true;
        }

        public override List<TicTacToeMove> GetPossibleMoves()
        {
            List<TicTacToeMove> moves = new List<TicTacToeMove>();
            for(int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (boardState[i, j] == -1)
                    {
                        TicTacToeMove move = new TicTacToeMove();
                        move.Cost = 1;
                        move.Utility = 1;
                        move.Row = i;
                        move.Col = (char)(j + 'A');
                        moves.Add(move);
                    }
                }
            }

            return moves;

        }
    }
}
