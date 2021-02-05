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
        protected Game Game { get; set; }
        protected char symbol;

        protected Player(string n, Game g)
        {
            name = n;
            Game = g;
        }
        public abstract string Name
        {
            get;
        }

        public abstract Move SelectMove(int[,] boardState);
    }
}
