using BoredGamesBot.Games.Common;
using BoredGamesBot.Games.TicTacToe;
using BoredGamesBot.Services;
using Discord.Addons.Interactive;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Discord.WebSocket;

namespace BoredGamesBot.Modules
{
    public class GameModule : ModuleBase<SocketCommandContext>
    {
        public GameService GameService { get; set; }
        public InteractiveService Interactivity { get; set; }

        [Command("ping")]
        [Alias("pong", "hello")]
        public Task PingAsync()
            => ReplyAsync("pong!");


        // Get info on a user, or the user who invoked the command if one is not specified
        [Command("bored")]
        public async Task BoredAsync()
        {
   
            //Board board = new TicTacToeBoard { };

            //string s = board.ToString();
            //Console.Write(s);
            //await ReplyAsync(s);
        }

        // Get info on a user, or the user who invoked the command if one is not specified
        [Command("test")]
        public async Task TaskAsync()
        {
            string s = "";
            for (int i =0; i < 128; i++)
            {
              //  s += i + ":\u3000";
                s += (char)i;
                s += ":\u2007";
                s += "\u2551";
                s += '\n';
            }


            await ReplyAsync(s);
        }

        // Get info on a user, or the user who invoked the command if one is not specified
        [Command("embed")]
        public async Task EmbedAysnc()
        {
           // Board board = new TicTacToeBoard { };

           // string s = board.ToString();

           // var embed = new EmbedBuilder
           // {
           //     // Embed property can be set within object initializer
           //     Title = "Tic Tac Toe",
           //     Description = s
           // }.Build();


           //await ReplyAsync(embed: embed);
        }

        [Command("play", RunMode = RunMode.Async)]
        public async Task Play(int index = 0)
        {
  
            string  reply;
            reply = await GameService.PlayAysnc(Context);

           await ReplyAsync(reply);
        }

        [Command("stop")]
        public async Task Stop(int index = 0)
        {

        }



       [Command("create")]
        public async Task CreateGame(IUser user = null)
        {
            string reply = "nuddo";
            if (user != null)
            {
               if( GameService.CreateNewGame(Context, Interactivity, user))
                {
                    reply = "Challage!";
                }
            }
            else
            {

                if (GameService.CreateNewGame(Context, Interactivity))
                {
                    reply = "Game Created";

                }
                else
                    reply = "Game not created";
            }


            await ReplyAsync(reply);


        }

        [Command("download", RunMode = RunMode.Async)]
        public async Task download()
        {
            await Context.Guild.DownloadUsersAsync();
            string reply = "download complere";
            

            await ReplyAsync(reply);


        }


        [Command("get")]
        public async Task GetUser(IUser user = null)
        {
         
            string reply = "nuddo";
            if (user != null)
            {
                reply = user.Username.ToString();
            }
            else
            {
                reply = "empty";
            }


            await ReplyAsync(reply);


        }

        //TODO CHANGE INPUT TO STTRING
        [Command("move", RunMode = RunMode.Async)]
        public async Task TakeMoveAsync(char col, int row)
        { 
            
            TicTacToeMove move = new TicTacToeMove();
            move.Cost = 1;
            move.Utility = 1;
            move.Row = row;
            move.Col = col;

            await ReplyAsync(GameService.AttemptMoveAsync(Context.User, move).Result);

            string reply;
            reply = await GameService.PlayAysnc(Context, false);
            if(reply != "")
                await ReplyAsync(reply);

        }

        [Command("create")]
        public async Task ChallangePlayer(String player)
        {
            
            switch (player) 
            {
                case "Random":
                    //add player
                    break;
                default:

                    break;
            }



            string reply;
            if (GameService.CreateNewGame(Context, Interactivity))
            {
                reply = "Game Created";

            }
            else
                reply = "Game not created";
            await ReplyAsync(reply);


        }
    }
}
