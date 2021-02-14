using BoredGamesBot.Games.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoredGamesBot.Games.Players
{
    class RandomPlayer<T> : IPlayer<T>
        where T : Move
    {
        public string Name { get; set; }
        public int Token { get; set; }
        public ulong Id { get; set; }

        public RandomPlayer(int token)
        {
            Name = "Randy";
            Token = token;
            Id = 0;
        }
        public async Task<T> SelectMoveAsync(Board<T> board)
        {
            Random rnd = new Random();
            List<T> possibleMoves =  board.GetPossibleMoves();
            T selected = possibleMoves[rnd.Next(0, possibleMoves.Count)];
            //selected.Token = Token;
            return selected;
        }
    }
}
