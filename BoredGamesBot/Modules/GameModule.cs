using BoredGamesBot.Games.Common;
using BoredGamesBot.Games.TicTacToe;
using BoredGamesBot.Services;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoredGamesBot.Modules
{
    public class GameModule : ModuleBase<SocketCommandContext>
    {
        public GameService GameService { get; set; }


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
        public async Task Play()
        {
            int reply;
            reply = await GameService.PlayAysnc(Context);

            await ReplyAsync(reply.ToString());


        }

        [Command("create")]
        public async Task CreateGame()
        {
            string reply;
            if (GameService.CreateNewGame(Context))
            {
                reply = "Game Created";

            }
            else
                reply = "Game not created";
            await ReplyAsync(reply);


        }

        [Command("move")]
        public async Task TakeMoveAsync(char col, int row)
        { 

            TicTacToeMove move = new TicTacToeMove();
            move.Cost = 1;
            move.Utility = 1;
            move.Row = row;
            move.Col = col;
            move.Token = 'X';

            await ReplyAsync(GameService.AttemptMove(Context.User, move));


        }
    }
}
