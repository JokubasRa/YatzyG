using System;
using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
    /// <summary>
    /// This is the class which performs game calculations
    /// </summary>
    public class Counter
    {
        public Counter(Player player)
        {
            if (player == null) throw new NullReferenceException();
            this.player = player;

        }

        private Player player { get; }

        public int GetTotalScore()
        {
            return player.Data.Sum(x => x.Value.Score);
        }

        public int Fives()
        {
            return player.PlayerDice.Where(x => x.Num == 5)
                .ToList()
                .Sum(x => x.Num);
        }

        public int Chance()
        {
            return player.PlayerDice.Sum(x => x.Num);
        }

        public int OnePair()
        {
            List<int> filtered = player.PlayerDice.GroupBy(x => x.Num)
                .Where(g => g.Count() == 2)
                .Select(y => y.Key)
                .ToList();

            if (filtered.Count == 1)
            {
                return filtered[0] * 2;
            }
            else return 0;
        }

    }
}

