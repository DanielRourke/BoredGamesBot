using System;
using System.Collections.Generic;
using System.Text;

namespace BoredGamesBot.Games.Common
{
    abstract class Game
    {
        public List<Player> players;
        public Board Board { get; set; }
        public int PlayerCount { get; set; }

        public abstract void Start();
        public abstract void Stop();
        public abstract void SelectStatingState();
    }
}
