using BoredGamesBot.Games.Common;
using Discord.WebSocket;
using Discord.Addons.Interactive;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Linq;
using System.Threading;

namespace BoredGamesBot.Games.Players
{
    public class DiscordUserPlayer<T> : IPlayer<T>
        where T : Move
    {
        public SocketCommandContext Context { get; set; } 
        public string Name { get; set; }
        public int Token { get; set; }
        public ulong Id { get; set; }

        public DiscordUserPlayer(SocketUser user, int token)
       {
            Id = user.Id;
            Token = token;
            Name = user.Username;
       }

        public DiscordUserPlayer(SocketCommandContext context, int token, InteractiveService interactivity)
        {
            Context = context;
            Interactive = interactivity;
            Id = Context.User.Id;
            Token = token;
            Name = Context.User.Username;
        }

        public DiscordUserPlayer(SocketCommandContext context, int token, InteractiveService interactivity , Discord.IUser user)
        {
            Context = context;
            Interactive = interactivity;
            Id = user.Id;
            Token = token;
            Name = user.Username;
        }


        public async Task<T> SelectMoveAsync(Board<T> board)
        {

            T move = (T)Activator.CreateInstance(typeof(T));
            move.Token = Token;

         //   await ReplyAsync(board.ToString() + $"\n {Name} , your turn!");
            var response = await NextMessageAsync(false);
            if (response != null)
            {
                if (response.Author.Id == Id && move.AttemptInit(response.Content))
                    return move;
                else 
                {
                    await ReplyAsync("That wasnt a Valid Move");
                }
            }
            else
                await ReplyAsync("You did not reply before the timeout use !move to add a move or !play to continue play");


            return null;
        }

        private async Task<Discord.Rest.RestUserMessage> ReplyAsync(string v)
        {
          return await Context.Channel.SendMessageAsync(v);
        }

        public InteractiveService Interactive { get; set; }

        public Task<SocketMessage> NextMessageAsync(ICriterion<SocketMessage> criterion, TimeSpan? timeout = null, CancellationToken token = default(CancellationToken))
            => Interactive.NextMessageAsync(Context, criterion, timeout, token);
        public Task<SocketMessage> NextMessageAsync(bool fromSourceUser = true, bool inSourceChannel = true, TimeSpan? timeout = null, CancellationToken token = default(CancellationToken))
            => Interactive.NextMessageAsync(Context, fromSourceUser, inSourceChannel, timeout, token);

        public Task<IUserMessage> ReplyAndDeleteAsync(string content, bool isTTS = false, Embed embed = null, TimeSpan? timeout = null, RequestOptions options = null)
            => Interactive.ReplyAndDeleteAsync(Context, content, isTTS, embed, timeout, options);

        //public Task<IUserMessage> PagedReplyAsync(IEnumerable<object> pages, bool fromSourceUser = true)
        //{
        //    var pager = new PaginatedMessage
        //    {
        //        Pages = pages
        //    };
        //    return PagedReplyAsync(pager, fromSourceUser);
        //}
        //public Task<IUserMessage> PagedReplyAsync(PaginatedMessage pager, bool fromSourceUser = true)
        //{
        //    var criterion = new Criteria<SocketReaction>();
        //    if (fromSourceUser)
        //        criterion.AddCriterion(new EnsureReactionFromSourceUserCriterion());
        //    return PagedReplyAsync(pager, criterion);
        //}
        public Task<IUserMessage> PagedReplyAsync(PaginatedMessage pager, ICriterion<SocketReaction> criterion)
            => Interactive.SendPaginatedMessageAsync(Context, pager, criterion);

        public RuntimeResult Ok(string reason = null) => new OkResult(reason);

    }
}
