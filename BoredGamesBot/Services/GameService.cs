using BoredGamesBot.Games.Common;
using BoredGamesBot.Games.Players;
using BoredGamesBot.Games.TicTacToe;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Addons.Interactive;
using Discord;

namespace BoredGamesBot.Services
{
    public class GameService
    {
        private readonly DiscordSocketClient _discord;

        private Dictionary<Guid, TicTacToe> CurrentGames;
        private Dictionary<ulong, List<Guid> > PlayersGames;


        public GameService(DiscordSocketClient discord)
        {
            CurrentGames = new Dictionary<Guid, TicTacToe>();
            PlayersGames = new Dictionary<ulong, List<Guid> >();
            _discord = discord;

            // Reaction added 
            //_discord.ReactionAdded
        }

        public async Task InitializeAsync()
        {

        }
        public async Task<bool> CreateNewGameAsync(SocketCommandContext Context, InteractiveService Interactivity)
        {
            Guid guid = Guid.NewGuid();

            //Initialise dictionary if this is users first game
            if (!PlayersGames.ContainsKey(Context.User.Id))
            {
                PlayersGames.Add(Context.User.Id, new List<Guid>());
            }

            //cant only play 5 games at time
            //if (playersGames[Context.User.Id].Count > 5)
            //{
            //    throw new NotImplementedException();
            //}

            PlayersGames[Context.User.Id].Add(guid);

            CurrentGames.Add(guid, new TicTacToe(Context, Interactivity));
            await CurrentGames[guid].StartAsync();
            return true;
        }

        public async Task<bool> CreateNewGameAsync(SocketCommandContext Context, InteractiveService Interactivity, IUser challanger = null)
        {
            Guid guid = Guid.NewGuid();

            //Initialise dictionary if this is users first game
            if (!PlayersGames.ContainsKey(Context.User.Id))
            {
                PlayersGames.Add(Context.User.Id, new List<Guid>());
            }

            //cant only play 5 games at time
            //if (playersGames[Context.User.Id].Count > 5)
            //{
            //    throw new NotImplementedException();
            //}

            PlayersGames[Context.User.Id].Add(guid);

            if(challanger != null)
            {
                if (!PlayersGames.ContainsKey(challanger.Id))
                {
                    PlayersGames.Add(challanger.Id, new List<Guid>());
                }
                PlayersGames[challanger.Id].Add(guid);
                CurrentGames.Add(guid, new TicTacToe(Context, Interactivity, challanger));
            }
            else
            {
                CurrentGames.Add(guid, new TicTacToe(Context, Interactivity));
            }

            await CurrentGames[guid].StartAsync();
            return true;
        }

        public async Task<string> PlayAysnc(ICommandContext Context, bool quickplay = true, int index = -1 )
        {
   
            if (!PlayersGames.TryGetValue(Context.User.Id, out List<Guid> Games))
            {
                return "Player not playing any games";
            }
            
            //get last added game
            if (index == -1) { index = Games.Count - 1; }
           
            if(!CurrentGames.TryGetValue(Games[index], out TicTacToe game))
            {
                return "$Player playing a game at index {index}";
            }

           
            var status = await game.PlayAsync(quickplay);

            if (status != TicTacToe.Status.Incomplete)
            {
                game.ConcludePlay();
                if (! RemoveGame(Games[index]))
               {
                    throw new NotImplementedException();
               }

                return "Game Completed";
            }

            // return Task.FromResult<int>(result);
            //   return result;

            if (!quickplay)
                return "";

            return "Quick Play Haulted";
        }

        private bool RemoveGame(Guid gameGuid)
        {
            if (!CurrentGames.TryGetValue(gameGuid, out TicTacToe game))
            {
                return false;
            }

            foreach (var player in game.GetPlayers())
            {
                //if player is not AI
                if (player.Id != 0)
                {
                    if (!PlayersGames.TryGetValue(player.Id, out List<Guid> CurrentPlayersGames))
                    {
                        return false;
                    }

                    CurrentPlayersGames.Remove(gameGuid);
                }

            }

             CurrentGames.Remove(gameGuid);

            return true;
        }


        public async Task<string> AttemptMoveAsync(SocketUser user, Move move, int index = -1)
        {
            //check if user playing a game
            if (!PlayersGames.TryGetValue(user.Id, out List<Guid> Games))
            {
                return "Player not playing any games";
            }

            //get last added game
            if (index == -1) { index = Games.Count - 1; }

            //Check if user is playing indexed game
            if (!CurrentGames.TryGetValue(Games[index], out TicTacToe game))
            {
                return "$Player playing a game at index {index}";
            }

            //ulong otherID = 0;
            //currentPVP.TryGetValue(user.Id, out otherID);

            //if (!(CurrentGames.TryGetValue(user.Id, out game) || CurrentGames.TryGetValue(otherID, out game)))
            //{
            //    return "not playing a game currently";
            //}

            if (game.AttemptMove((TicTacToeMove)move, user.Id))
            {
                return "Move applied";
            }

            return "Failed to apply move";
        }



        //public bool PlayingGame(SocketUser user)
        //{
        //    return PlayersGames.ContainsKey(user.Id);
        //}



    }
}
