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

        protected Player(string n)
        {
            name = n;
            id = 0;
        }
        public abstract string Name
        {
            get;
        }

        public abstract ulong ID
        {
            get;
        }

        public abstract void TakeTurn(Board board);
    }
}
