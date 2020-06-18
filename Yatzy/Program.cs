using System;

namespace Yatzy
{
    /// <summary>
    /// This class contains main method.
    /// </summary>
    public class Program
    {
       
        public static void Main(string[] args)
        {
            Console.WriteLine("This is Yatzi dice game.\nEnter the number of players");
            int n = ReadNumber();
            Game game = new Game(n);
            game.GameLoop();
        }
        public static int ReadNumber()
        {
            int n;
            string input = Console.ReadLine();
            while (!Int32.TryParse(input, out n) || n < 1)
            {
                Console.WriteLine("Invalid input: Could not recognise a valid number. Try Again.");
                input = Console.ReadLine();
            }
            return n;
        }
    }
}
