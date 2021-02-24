using BoredGamesBot.Games.Common;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoredGamesBot.Games.Common
{
    public interface IPlayer<T> 
        where T : Move
    {
        string Name { get; set; }
        int Token { get; set; }
        ulong Id { get; set; }
        public abstract Task<T> SelectMoveAsync(Board<T> board, CancellationToken token);
    }
}
