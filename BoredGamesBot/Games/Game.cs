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
        public abstract void Start();
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

        public virtual bool AttemptMove(T m)
        {
            if (board.ValidMove(m))
            {
                board.UpdateBoard(m);
                return true;
            }

            return false;
        }

        public string BoardToString()
        {
            return board.ToString();
        }

        public int Play()
        {
            
            playing = true;
            while (playing && board.GetPossibleMoves().Count > 0)
            {
                T move = players[playerTurn].SelectMoveAsync(board);

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
