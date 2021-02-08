using System;
using System.Collections.Generic;
using System.Text;

namespace BoredGamesBot.Games.Common
{
    public abstract class Move
    {
        public int Token { get; set; }
        public int Cost { get; set; }
        public int Utility { get; set; }

    }
}
