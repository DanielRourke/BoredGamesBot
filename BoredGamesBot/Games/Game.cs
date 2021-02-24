using BoredGamesBot.Games.Players;
using Discord.Addons.Interactive;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoredGamesBot.Games.Common
{
    abstract class Game<T>
        where T : Move
    {
        protected List<IPlayer<T>> players;
        protected Board<T> board;
        public SocketCommandContext Context { get; set; }
        public Discord.Rest.RestUserMessage GameDisplay { get; set; }
        public InteractiveService Interactive { get; set; }

        protected int playerTurn; 
        public bool QuickPlay { get; set; }
        protected int winner;

        public enum Status { Incomplete = -1,  Draw, WinOne, WinTwo};

        public Game(SocketCommandContext context, InteractiveService interactive)
        {
            Context = context;
            players = new List<IPlayer<T>>();
            QuickPlay = true;
            Interactive = interactive;
        }
        
        public virtual async Task StartAsync()
        {
            GameDisplay = await Context.Channel.SendMessageAsync("Base Game");

            await GameDisplay.PinAsync();
            await GameDisplay.ModifyAsync(msg => msg.Content = "test [edited]");



            //GameDisplay = Context.Channel.S
        }

        //public void Start()
        //{
        //    playing = true;

        //    T move = players[playerTurn].SelectMoveAsync(board);



        //        //not sure this should be here
        //        if (move == null)
        //        {
        //            playing = false;
        //        }

        //        if (board.ValidMove(move))
        //        {
        //            board.UpdateBoard(move);
        //           // SelectNextPlayer();
        //        }


        //}

        //public void Next()
        //{


        //    playing = true;

        //    T move = await players[playerTurn].SelectMoveAsync(board);



        //    //not sure this should be here
        //    if (move == null)
        //    {
        //        playing = false;
        //    }

        //    if (board.ValidMove(move))
        //    {
        //        board.UpdateBoard(move);
        //        // SelectNextPlayer();
        //    }


        //  }
        public void Stop()
        {
            QuickPlay = false;
        }
        public abstract void SelectStatingState();

        public virtual void SelectNextPlayer()
        {
            playerTurn = (playerTurn + 1) % players.Count;
            GameDisplay.ModifyAsync(msg => msg.Content = board.ToString() + $"\n Current it's {players[playerTurn].Name} turn");
        }

        public bool PlayerTurn(IPlayer<T> p) => players[playerTurn] == p;

        //public virtual bool AttemptMove(T m)
        //{
        //    if (board.ValidMove(m))
        //    {
        //        board.UpdateBoard(m);
        //        return true;
        //    }

        //    return false;
        //}

        public virtual bool AttemptMove(T m, ulong Id)
        {
            if( GetPlayerIndex(Id) == playerTurn )
            {
                m.Token = players[playerTurn].Token;
                return  AttemptMove(m);
            }

            return false;
        }

        public virtual bool AttemptMove(T m)
        {
            if (board.ValidMove(m))
            {
                board.UpdateBoard(m);
                SelectNextPlayer();
                return true;
            }

            return false;
        }

        //public virtual bool AttemptMove(T m)
        //{
        //    if (board.ValidMove(m))
        //    {
        //        board.UpdateBoard(m);
        //        if (board.GetPossibleMoves().Count == 0)
        //            playing = false;
        //        SelectNextPlayer();
        //        T move = players[playerTurn].SelectMoveAsync(board);


        //        //not sure this should be here
        //                if (move == null)
        //        {
        //            playing = false;
        //        }

        //        if (board.ValidMove(move))
        //        {
        //            board.UpdateBoard(move);
        //            SelectNextPlayer();
        //        }
        //        T s = players[playerTurn].SelectMoveAsync(board);
        //        return true;
        //    }

        //    return false;
        //}

        public string BoardToString()
        {
            return board.ToString();
        }

        //public int Play()
        //{

        //    playing = true;
        //    while (playing && board.GetPossibleMoves().Count > 0)
        //    {
        //        T move = players[playerTurn].SelectMoveAsync(board);

        //        //not sure this should be here
        //        if (move == null)
        //        {
        //            playing = false;
        //        }

        //        if (board.ValidMove(move))
        //        {
        //            board.UpdateBoard(move);
        //            SelectNextPlayer();
        //        }
        //    }


        //    return playerTurn;
        //}

        public async Task<Status> PlayAsync(bool continuous = true)
        {

           
            
            while ((continuous || players[playerTurn].GetType() != typeof(DiscordUserPlayer<T>)) && GameStatus() == Status.Incomplete)
            {
                 await GameDisplay.ModifyAsync(msg => msg.Content = board.ToString() + $"\n Current it's {players[playerTurn].Name} turn");
                //await Context.Channel.SendMessageAsync(board.ToString() + $"\n Current it's {players[playerTurn].Name} turn");

                CancellationTokenSource cts = new CancellationTokenSource();

              //  cts.CancelAfter(60000);
                Task<T> select = players[playerTurn].SelectMoveAsync(board, cts.Token);
                Task<T> react = awaitReactionMoveAsync(cts.Token);

                //  T move = await players[playerTurn].SelectMoveAsync(board);

                Task < T > getMove =  await Task.WhenAny(select, react);

                cts.Cancel();

                //not sure this should be here
                if (getMove.Result == null)
                {
                    continuous = false;
                }
                else
                {
                    AttemptMove(getMove.Result);
                }
    
            }
         
            return GameStatus();
        }

        public async Task ContinuePlay()
        {
            SelectNextPlayer();
            await PlayAsync(false);
        }

        public abstract void ConcludePlay();


        public abstract Status GameStatus();

        //public async Task NotifyBotAsync()
        //{
        //    if(players[playerTurn].GetType() != typeof(DiscordUserPlayer<T>))
        //    {
        //        T move = await players[playerTurn].SelectMoveAsync(board);

        //        await AttemptMove(move);
        //    }
        //}

        public int GetPlayerIndex(ulong id)
        {
            for (int k = 0; k < players.Count; k++)
            {
                if (players[k].GetType() == typeof(DiscordUserPlayer<T>) &&
                    ((DiscordUserPlayer<T>)players[k]).Id == id )
                {
                    return k;
                }
            }

            return -1;
        }

        public IPlayer<T> GetPlayer(ulong id)
        {
            for (int k = 0; k < players.Count; k++)
            {
                if (players[k].GetType() == typeof(DiscordUserPlayer<T>) &&
                    ((DiscordUserPlayer<T>)players[k]).Id == id)
                {
                    return players[k];
                }
            }

            return null;
        }

        public IPlayer<T> GetPlayer(int index)
        {
            if (index < 0 || index >= players.Count)
                return null;

            return players[index];
        }

        public List<IPlayer<T>> GetPlayers()
        {
            return players;
        }


        public bool isPlayersTurn(ulong id)
        {
            return GetPlayerIndex(id) == playerTurn;
        }

        public abstract Task<T> awaitReactionMoveAsync(CancellationToken token);


    }
}
