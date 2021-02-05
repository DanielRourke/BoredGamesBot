using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;

namespace BoredGamesBot.Games.Common
{
    class DiscordUserPlayer : Player
    {
        public override string Name => name;

        public DiscordUserPlayer(SocketUser user, char symbol, Game g) : base(user.ToString(),g )
        {
            id = user.Id;
            this.symbol = symbol;
        }
        public override Move SelectMove(int[,] boardState)
        {
            List<Move> moves = Game.Board.GetPossibleMoves();
            return moves[0];
        }
    }
}
