using System;
using System.Collections.Generic;
using System.Text;

namespace BoredGamesBot.Games.Common
{
    abstract class Game
    {
        private List<Player> players;
        private Board board;
        private int playerCount;
        public abstract void Start();
        public abstract void Stop();
        public abstract void SelectStatingState();
    }
}
