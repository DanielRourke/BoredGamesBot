﻿using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoredGamesBot.Services
{
    class GameService
    {

        private readonly DiscordSocketClient _discord;

        public GameService(DiscordSocketClient discord)
        {
           
            _discord = discord;

            // Reaction added 
            //_discord.ReactionAdded
        }

        public async Task InitializeAsync()
        {

        }

        public async Task CreateGameAsync()
        {

        }


    }
}
