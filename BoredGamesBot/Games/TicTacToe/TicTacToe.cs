using BoredGamesBot.Games.Common;
using BoredGamesBot.Games.Players;
using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        public TicTacToe(SocketCommandContext context, InteractiveService interactivity): base(context, interactivity)
        {
            players.Add(new DiscordUserPlayer<TicTacToeMove>(context, 'X', interactivity));
            players.Add(new RandomPlayer<TicTacToeMove>('O'));
            playerTurn = 0;
            board = new TicTacToeBoard();
        }

        public TicTacToe(SocketCommandContext context, InteractiveService interactivity, Discord.IUser challenger) : base(context, interactivity)
        {
            players.Add(new DiscordUserPlayer<TicTacToeMove>(context, 'X', interactivity));
            players.Add(new DiscordUserPlayer<TicTacToeMove>(context, 'O', interactivity, challenger));
            playerTurn = 0;
            board = new TicTacToeBoard();
        }

        public override async Task StartAsync()
        {
            SelectStatingState();

            Emoji[] emojis = new Emoji[7];
            emojis[0] = new Emoji("\uD83C\uDDE6"); //A
            emojis[1] = new Emoji("\uD83C\uDDE7"); //B
            emojis[2] = new Emoji("\uD83C\uDDE8"); //C
            emojis[3] = new Emoji("\u0030\u20E3"); //0
            emojis[4] = new Emoji("\u0031\u20E3"); //1
            emojis[5] = new Emoji("\u0032\u20E3"); //2
            emojis[6] = new Emoji("\uD83C\uDD97"); //OK

   

            GameDisplay = await Context.Channel.SendMessageAsync(board.ToString());


            foreach (var emoji in emojis)
            {
                await GameDisplay.AddReactionAsync(emoji);
            }


           // await GameDisplay.AddReactionAsync(emojis[0]);
           //await GameDisplay.PinAsync();

 
        }

        public override void ConcludePlay()
        {
         //   GameDisplay.ModifyAsync(msg => msg.Content = board.ToString() + $"\n Current it's {players[playerTurn].Name} turn");

            int winner = (int)GameStatus();
            if ((Game<TicTacToeMove>.Status)winner == Game<TicTacToeMove>.Status.Draw)
                GameDisplay.ModifyAsync(msg => msg.Content = board.ToString() + $"\n Game has concluded in a Draw");
            else
                GameDisplay.ModifyAsync(msg => msg.Content = board.ToString() + $"\n Player { players[winner - 1].Name} Wins");
        }

        public override Status GameStatus()
        {
            int[,] state = board.GetBoardState();

            int height = state.GetLength(0);
            int width = state.GetLength(1);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                   if(  threeInRow(i, j, 0, 0, 1, state, state[i,j]) ||
                        threeInRow(i, j, 0, 1, 0, state, state[i, j]) ||
                        threeInRow(i, j, 0, 1, 1, state, state[i, j]) ||
                        threeInRow(i, j, 0, 1, -1, state, state[i, j]))
                    {
                        for(int k =0; k < players.Count; k++)
                        {
                            if(players[k].Token == state[i,j])
                            {
                                winner = k;
                                return (Game<TicTacToeMove>.Status)(k + 1);
                            }
                        }
                    }
                }
            }

            if (board.GetPossibleMoves().Count == 0)
                return Game<TicTacToeMove>.Status.Draw;

            return Game<TicTacToeMove>.Status.Incomplete;
        }

        private bool threeInRow(int i, int j, int count, int iDirection, int jDirection, int[,] state, int symbol)
        {

            if (i < 0 || i >= state.GetLength(0) || j < 0 || j >= state.GetLength(1) || state[i, j] != symbol || symbol == -1)
                return false;
            else if (count == 2)
                return true;


            return threeInRow(i + iDirection, j + jDirection, count + 1, iDirection, jDirection, state, symbol);
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

        public override async Task<TicTacToeMove> awaitReactionMoveAsync(CancellationToken token)
        {

            TicTacToeReactionCallback callback = new TicTacToeReactionCallback(players[playerTurn], board, GameDisplay);


            Interactive.AddReactionCallback(GameDisplay, callback);

            //slow poll;
            while(!callback.Complete || token.IsCancellationRequested )
            {
                await Task.Delay(25);
            }

            Interactive.ClearReactionCallbacks();

            return callback.Move;

        }
    }
}
