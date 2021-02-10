using BoredGamesBot.Games.Common;
using BoredGamesBot.Games.Players;
using Discord.Commands;
using Discord.WebSocket;
using Interactivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoredGamesBot.Games.TicTacToe
{
    class TicTacToe : Game<TicTacToeMove>
    {
       // public TicTacToe(SocketUser p1, SocketUser p2)
        //{
        //    players.Add(new DiscordUserPlayer<TicTacToeMove>(p1, 'X'));
        //    players.Add(new DiscordUserPlayer<TicTacToeMove>(p1, '0'));
        //    playerTurn = 0;
        //    board = new TicTacToeBoard();
        //}


        //public TicTacToe(SocketUser p1)
        //{
        //    players.Add(new DiscordUserPlayer<TicTacToeMove>(p1,  'X'));
        //    players.Add(new RandomPlayer<TicTacToeMove>('0') );
        //    playerTurn = 0;
        //    board = new TicTacToeBoard();
        //}

        public TicTacToe(ICommandContext context, InteractivityService interactivity)
        {
            players.Add(new DiscordUserPlayer<TicTacToeMove, ICommandContext>(context, 'X', interactivity));
            players.Add(new RandomPlayer<TicTacToeMove>('O'));
            playerTurn = 0;
            board = new TicTacToeBoard();
        }

        public override void SelectStatingState()
        {
            board.SetBoardState(-1);
        }


        public void Start() 
        {
           SelectStatingState();
            board.ToString();
        }
    }
}
