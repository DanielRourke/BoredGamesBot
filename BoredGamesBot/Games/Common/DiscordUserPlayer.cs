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
        public override ulong ID => id;

        private SocketUser player;

        public DiscordUserPlayer(SocketUser user) : base(user.ToString())
        {
            id = user.Id;
            player = user;
        }
        public override void TakeTurn(Board board)
        {
            List<Move> moves = board.GetPossibleMoves();
            player.
        }
    }
}
