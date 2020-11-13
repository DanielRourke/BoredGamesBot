using BoredGamesBot.Games.Common;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace BoredGamesBot.Games.Common
{
    abstract class Player
    {
        protected string name;
        protected ulong id;
        protected Board board;
        protected char symbol;

        protected Player(string n)
        {
            name = n;
        }
        public abstract string Name
        {
            get;
        }

        public abstract Move SelectMove(int[,] boardState);
    }
}
