using BoredGamesBot.Games.Common;
using BoredGamesBot.Games.Players;
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
    public class EnsureFromIdCriterion : ICriterion<SocketReaction>
    {
        private readonly ulong _id;

        public EnsureFromIdCriterion(ulong id)
            => _id = id;

        public Task<bool> JudgeAsync(SocketCommandContext sourceContext, SocketReaction parameter)
        {
            bool ok = _id == parameter.UserId;
            return Task.FromResult(ok);
        }
    }

    class TicTacToeReactionCallback : IReactionCallback

    {
        public TicTacToeMove Move;
        Board<TicTacToeMove> board;
        public bool Complete = false;
        public EnsureFromIdCriterion criteria;
        private IPlayer<TicTacToeMove> player;
       
        Discord.Rest.RestUserMessage GameDisplay;

        //public TicTacToeReactionCallback(IPlayer<TicTacToeMove> player, TicTacToeBoard ticTacToe)
        //{
        //    this.player = player;
        //    this.board = ticTacToe;
        //    criteria  = new EnsureFromIdCriterion(player.Id);
        //}

        public TicTacToeReactionCallback(IPlayer<TicTacToeMove> player, Board<TicTacToeMove> board, Discord.Rest.RestUserMessage gameDisplay)
        {
            this.player = player;
            this.board = board;
            criteria = new EnsureFromIdCriterion(player.Id);
            Move = new TicTacToeMove();
            Move.Cost = 1;
            Move.Utility = 1;
            Move.Token = player.Token;
            GameDisplay = gameDisplay;
        }

        public ICriterion<SocketReaction> Criterion => criteria;

        //the
        public RunMode RunMode => RunMode.Async;

        public TimeSpan? Timeout => TimeSpan.FromSeconds(60.0);

        public SocketCommandContext Context { get; }

        //this is what happens when they add a reaction
        public async Task<bool> HandleCallbackAsync(SocketReaction reaction)
        {
          

           // Move.AttemptInit("A 1");
            switch (reaction.Emote.Name)
            {
                case "\uD83C\uDDE6":     // A
                    Move.Col = 'A';
                    break;
                case "\uD83C\uDDE7":     // B
                    Move.Col = 'B';
                    break;
                case "\uD83C\uDDE8":     // C
                    Move.Col = 'C';
                    break;
                case "\u0030\u20E3":          // 3
                    Move.Row = 0;
                    break;
                case "\u0031\u20E3":           // 1️
                    Move.Row = 1;
                    break;
                case "\u0032\u20E3":          // 2
                    Move.Row = 2;
                    break;
                case "\uD83C\uDD97":    // 🆗
                    Complete = board.ValidMove(Move);
                    break;
                    
     
            }


            await  GameDisplay.RemoveReactionAsync(reaction.Emote, player.Id);
            //game.AttemptMove(Move);

            return false;
        }
    }

}
