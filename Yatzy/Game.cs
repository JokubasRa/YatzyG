using System;
using System.Collections.Generic;
using System.Linq;

namespace Yatzy
{
    /// <summary>
    /// This class contains a game loop. 
    /// </summary>
    public class Game
    {
        public Game(int numOfPlayers)
        {
            if (numOfPlayers < 1) throw new NullReferenceException();
            Players = Enumerable.Range(1, numOfPlayers).Select(i => new Player(i)).ToList();
        }
        public List<Player> Players { get; private set; }


        // This method runs while any player has an unused scoring property
        public void GameLoop()
        {
            while (!EndOfGame())
            {
                foreach (Player player in Players)
                {
                    player.Turn();
                }
            }
            PrintResults();
        }


        private bool EndOfGame()
        {
            return Players.All(x => x.EndOfGame());
        }

        //This method is supposed to print game results at the end of game
        private void PrintResults()
        {
            Console.WriteLine("Results: ");
            Players.ForEach(x => Console.WriteLine("{0} Player scored {1}", x.Index, new Counter(x).GetTotalScore()));
        }
    }
}
