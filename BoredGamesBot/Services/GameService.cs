using BoredGamesBot.Games.Common;
using BoredGamesBot.Games.TicTacToe;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoredGamesBot.Services
{
    class GameService
    {
        //  Dictionary<int, Game> CurrentGames;
        //   Dictionary<int, LinkedList<int>> PlayerCurrentGames;
        Game game;
        private readonly DiscordSocketClient _discord;

        public GameService(DiscordSocketClient discord)
        {
           
            _discord = discord;
            
            // Reaction added 
            //_discord.ReactionAdded
        }

        public void Initialize()
        {
           // CurrentGames = new  Dictionary<int, Game>();
          //  PlayerCurrentGames = new Dictionary<int, LinkedList<int>>();
        }

        public async Task CreateGameAsync()
        {

            //CurrentGames.Add();

            game = new TicTacToeGame();

        }






    }
}
