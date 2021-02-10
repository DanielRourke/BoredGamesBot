using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoredGamesBot.Games.Common
{
    abstract class Game<T>
        where T : Move
    {
        protected List<IPlayer<T>> players;
        protected Board<T> board;

        protected int playerTurn; 
        protected bool playing;

        public Game()
        {
            players = new List<IPlayer<T>>();
        }
        //public void Start()
        //{
        //    playing = true;

        //    T move = players[playerTurn].SelectMoveAsync(board);



        //        //not sure this should be here
        //        if (move == null)
        //        {
        //            playing = false;
        //        }

        //        if (board.ValidMove(move))
        //        {
        //            board.UpdateBoard(move);
        //           // SelectNextPlayer();
        //        }


        //}

        //public void Next()
        //{


        //    playing = true;

        //    T move = await players[playerTurn].SelectMoveAsync(board);



        //    //not sure this should be here
        //    if (move == null)
        //    {
        //        playing = false;
        //    }

        //    if (board.ValidMove(move))
        //    {
        //        board.UpdateBoard(move);
        //        // SelectNextPlayer();
        //    }


      //  }
        public void Stop()
        {
            playing = false;
        }
        public abstract void SelectStatingState();

        public virtual void SelectNextPlayer()
        {
            playerTurn = (playerTurn + 1) % players.Count;
        }

        public bool PlayerTurn(IPlayer<T> p) => players[playerTurn] == p;

        //public virtual bool AttemptMove(T m)
        //{
        //    if (board.ValidMove(m))
        //    {
        //        board.UpdateBoard(m);
        //        return true;
        //    }

        //    return false;
        //}

        public virtual bool AttemptMove(T m)
        {
            if (board.ValidMove(m))
            {
                board.UpdateBoard(m);
                //isGame over
                //declare winner

                SelectNextPlayer();

                return true;
            }

            return false;
        }

        //public virtual bool AttemptMove(T m)
        //{
        //    if (board.ValidMove(m))
        //    {
        //        board.UpdateBoard(m);
        //        if (board.GetPossibleMoves().Count == 0)
        //            playing = false;
        //        SelectNextPlayer();
        //        T move = players[playerTurn].SelectMoveAsync(board);


        //        //not sure this should be here
        //                if (move == null)
        //        {
        //            playing = false;
        //        }

        //        if (board.ValidMove(move))
        //        {
        //            board.UpdateBoard(move);
        //            SelectNextPlayer();
        //        }
        //        T s = players[playerTurn].SelectMoveAsync(board);
        //        return true;
        //    }

        //    return false;
        //}

        public string BoardToString()
        {
            return board.ToString();
        }

        //public int Play()
        //{

        //    playing = true;
        //    while (playing && board.GetPossibleMoves().Count > 0)
        //    {
        //        T move = players[playerTurn].SelectMoveAsync(board);

        //        //not sure this should be here
        //        if (move == null)
        //        {
        //            playing = false;
        //        }

        //        if (board.ValidMove(move))
        //        {
        //            board.UpdateBoard(move);
        //            SelectNextPlayer();
        //        }
        //    }


        //    return playerTurn;
        //}

        public async Task<int> PlayAsync()
        {

            playing = true;
            while (playing && board.GetPossibleMoves().Count > 0)
            {
                T move = await players[playerTurn].SelectMoveAsync(board);

                //not sure this should be here
                if (move == null)
                {
                    playing = false;
                }

                if (board.ValidMove(move))
                {
                    board.UpdateBoard(move);
                    SelectNextPlayer();
                }
            }


            return playerTurn;
        }





    }
}
