using BoredGamesBot.Games.Common;
using Discord.WebSocket;
using Interactivity;
using Interactivity.Selection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace BoredGamesBot.Games.Players
{
    public class DiscordUserPlayer<T, U> : IPlayer<T>
        where T : Move
        where U : class, ICommandContext
    {

        public U Context { get; set; }
        public InteractivityService Interactivity { get; set; }
        public string Name { get; set; }
        public int Token { get; set; }
        protected ulong Id { get; set; }

       public DiscordUserPlayer(SocketUser user, int token)
       {
            Id = user.Id;
            Token = token;
            Name = ToString();
       }

        public DiscordUserPlayer(U context, int token )
        {

            Context = context;
            Id = Context.User.Id;
            Token = token;
            Name = ToString();
        }

        public T SelectMoveAsync(Board<T> board)
        {
            Context.Channel.SendMessageAsync(board.ToString());
            ExampleReactionSelectionAsync();
            return board.GetPossibleMoves()[0];
        }

        public async Task ExampleMessageSelectionAsync()
        {
            var builder = new MessageSelectionBuilder<string>()
                .WithValues("Hi", "How", "Hey", "Huh?!")
                .WithUsers((SocketUser)Context.User)
                .WithDeletion(DeletionOptions.AfterCapturedContext | DeletionOptions.Invalids);

            var result = await Interactivity.SendSelectionAsync(builder.Build(), Context.Channel, TimeSpan.FromSeconds(50));

            if (result.IsSuccess == true)
            {
                await Context.Channel.SendMessageAsync(result.Value.ToString());
            }
        }

        public async Task ExampleReactionSelectionAsync()
        {
            var builder = new ReactionSelectionBuilder<string>()
                .WithValues("Hi", "How", "Hey", "Huh?!")
                .WithEmotes(new Emoji("💵"), new Emoji("🍭"), new Emoji("😩"), new Emoji("💠"))
                .WithUsers((SocketUser)Context.User)
                .WithDeletion(DeletionOptions.AfterCapturedContext | DeletionOptions.Invalids);

            var result = await Interactivity.SendSelectionAsync(builder.Build(), Context.Channel, TimeSpan.FromSeconds(50));

            if (result.IsSuccess == true)
            {
                await Context.Channel.SendMessageAsync(result.Value.ToString());
            }
        }
    }
}
