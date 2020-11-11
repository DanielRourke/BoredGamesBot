using BoredGamesBot.Games.Common;
using BoredGamesBot.Games.TicTacToe;
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


        [Command("ping")]
        [Alias("pong", "hello")]
        public Task PingAsync()
    => ReplyAsync("pong!");


        // Get info on a user, or the user who invoked the command if one is not specified
        [Command("bored")]
        public async Task BoredAsync()
        {
   
            Board board = new TicTacToeBoard { };

            string s = board.ToString();
            Console.Write(s);
            await ReplyAsync(s);
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
            Board board = new TicTacToeBoard { };

            string s = board.ToString();

            var embed = new EmbedBuilder
            {
                // Embed property can be set within object initializer
                Title = "Tic Tac Toe",
                Description = s
            }.Build();

    
           

                await ReplyAsync(embed: embed);
        }
    }
}
