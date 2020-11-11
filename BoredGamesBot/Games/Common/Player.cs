using BoredGamesBot.Games.Common;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace BoredGamesBot.Games.Common
{
    abstract class Player
    {
        private string name;
        private Board board;
        public abstract Move SelectMove(int[,] boardState);
    }
}
