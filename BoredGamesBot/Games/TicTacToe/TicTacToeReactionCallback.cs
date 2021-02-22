using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoredGamesBot.Games.TicTacToe
{
    class TicTacToeReactionCallback : IReactionCallback

    {
        TicTacToeMove Move;
        TicTacToe game;
        SocketCommandContext context;

        public EnsureFromUserCriterion criterion;
        public ICriterion<SocketReaction> Criterion => (ICriterion<SocketReaction>)criterion;

        TicTacToeReactionCallback(IUser user, Game game)
        {
            this.game = game;
            criterion = new EnsureFromUserCriterion(user);
        }
        //the
        public RunMode RunMode => RunMode.Async;

        public TimeSpan? Timeout => TimeSpan.FromSeconds(60.0);

        public SocketCommandContext Context => context;

        //this is what happens when they add a reaction
        public Task<bool> HandleCallbackAsync(SocketReaction reaction)
        {
            game.AttemptMove(Move);
        }
    }

}
