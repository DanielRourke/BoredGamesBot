using System;
using System.Collections.Generic;
using System.Text;

namespace BoredGamesBot.Games.Common
{
   interface Game
    {
        //public List<Player> players;
        //public Board Board { get; set; }
        //public int PlayerCount { get; set; }

        public void Begin();
        public void Conclude();
        public void isGameOver();
        public void SetUp();
    }
}
