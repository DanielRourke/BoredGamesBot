using BoredGamesBot.Games.Common;
using BoredGamesBot.Games.Players;
using BoredGamesBot.Games.TicTacToe;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoredGamesBot.Services
{
    public class GameService
    {
        private readonly DiscordSocketClient _discord;

        private Dictionary<ulong, TicTacToe> currentGames;

        public GameService(DiscordSocketClient discord)
        {
            currentGames = new Dictionary<ulong, TicTacToe>();
            _discord = discord;

            // Reaction added 
            //_discord.ReactionAdded
        }

        public async Task InitializeAsync()
        {

        }


        public Task<int> PlayAysnc(ICommandContext Context)
        {
           
            int result =currentGames.GetValueOrDefault(Context.User.Id).Play();

            return Task.FromResult<int>(result);
        }

        public bool CreateNewGame(ICommandContext Context)
        {
            currentGames.Add(Context.User.Id, new TicTacToe(Context));
            currentGames.GetValueOrDefault(Context.User.Id).Start();
      
            return true;
        }

        public string AttemptMove(SocketUser user, Move move)
        {
            TicTacToe game;

            if (!currentGames.TryGetValue(user.Id, out game))
            {
                return "not playing a game currently";
            }

            if (game.AttemptMove((TicTacToeMove)move))
            {
                return "Move applied" + game.BoardToString(); 
            }

            return "Failed to apply move";
        }




        public bool PlayingGame(SocketUser user)
        {
            return currentGames.ContainsKey(user.Id);
        }

    }
}
