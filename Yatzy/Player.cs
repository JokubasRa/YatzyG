using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Yatzy
{
    /// <summary>
    /// This class contains information about a player
    /// </summary>
    public class Player
    {
        private static readonly int NUM_OF_DICE = 5;
        private static readonly int TIMES_TO_REROLL = 2;
        public enum ScoringType { FIVES, CHANCE, ONE_PAIR }

        public Player(int Index)
        {
            if (Index < 1) { throw new ArgumentOutOfRangeException(); }
            this.Index = Index;
            DiceToReroll = new List<Dice>();
            PlayerDice = Enumerable.Range(1, NUM_OF_DICE).Select(i => new Dice(i)).ToList();
            EndOfThrowing = false;

            Data = new Dictionary<ScoringType, MutableTuple>();
            foreach (ScoringType scoringType in Enum.GetValues(typeof(ScoringType)))
            {

                Data.Add(scoringType, new MutableTuple());

            }
        }




        private List<Dice> DiceToReroll { get; set; }
        public int Index { get; }
        public List<Dice> PlayerDice { get; private set; }
        private bool EndOfThrowing { get; set; }
        public Dictionary<ScoringType, MutableTuple> Data { get; set; }



        public void Turn()
        {
            Console.WriteLine("It's {0} player's turn", Index);
            PlayerDice.ForEach(x => x.Roll());
            string input;
            for (int counter = 0; counter < TIMES_TO_REROLL && !EndOfThrowing; counter++)
            {

                Console.Write("Your roll: ");
                PlayerDice.ForEach(x => Console.Write(x.Num + " "));
                Console.WriteLine("\nPlease write the index(es) of dice to reroll. If your do not wish to reroll, type 0.");
                input = Console.ReadLine();
                while (!ParseInputReroll(input))
                {
                    input = Console.ReadLine();
                }
                Reroll();
            }
            Console.Write("Your last roll was: ");
            PlayerDice.ForEach(x => Console.Write(x.Num + " "));
            Console.WriteLine("\nPlease choose in which category you wish to score. Write FIVES, CHANCE or ONE_PAIR");
            input = Console.ReadLine();
            while (!ParseInputScoring(input))
            {
                input = Console.ReadLine();
            }
            Console.Write("\n");

            PrintPlayerTable();

            EndOfThrowing = false;
            Console.Write("\n");
        }

        private void PrintPlayerTable()
        {
            StringBuilder str = new StringBuilder();
            foreach (ScoringType scoringType in Enum.GetValues(typeof(ScoringType)))
            {
                str.Append(scoringType + new string(' ', 15 - (Enum.GetName(typeof(ScoringType), scoringType)).Length));

                if (Data[scoringType].IsUsed)
                {
                    str.Append("Used       ");
                }
                else str.Append("Not used   ");

                str.Append(Data[scoringType].Score + "\n");

            }
            Console.Write(str);
        }


        public bool ParseInputReroll(string line)
        {
            if (line == null) throw new NullReferenceException();
            if (line.Trim().Equals("0"))
            {
                DiceToReroll.Clear();
                EndOfThrowing = true;
                return true;
            }

            List<int> InputNumbers;
            try
            {
                InputNumbers = line.Split().Select(int.Parse).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Input: Could not recognise numbers. Try again.");
                return false;
            }

            if ((InputNumbers.Count() > NUM_OF_DICE || InputNumbers.Count() < 1) || InputNumbers.Any(x => x > NUM_OF_DICE || x < 1))
            {
                Console.WriteLine("Invalid input: Too many entered numbers or they are out of range. Try again.");
                return false;
            }

            DiceToReroll = PlayerDice.Where(x => InputNumbers.Contains(x.IndexOfDice)).ToList();
            return true;
        }
        public bool ParseInputScoring(string line)
        {
            if (line == null) throw new NullReferenceException();
            line.Trim();
            switch (line)
            {
                case "FIVES":
                    if (Data[ScoringType.FIVES].IsUsed) return InvalidCommand();
                    Data[ScoringType.FIVES].IsUsed = true;
                    Data[ScoringType.FIVES].Score = new Counter(this).Fives();
                    return true;
                case "CHANCE":
                    if (Data[ScoringType.CHANCE].IsUsed) return InvalidCommand();
                    Data[ScoringType.CHANCE].IsUsed = true;
                    Data[ScoringType.CHANCE].Score = new Counter(this).Chance();
                    return true;
                case "ONE_PAIR":
                    if (Data[ScoringType.ONE_PAIR].IsUsed) return InvalidCommand();
                    Data[ScoringType.ONE_PAIR].IsUsed = true;
                    Data[ScoringType.ONE_PAIR].Score = new Counter(this).OnePair();
                    return true;
                default:
                    return InvalidCommand();
            }

        }

        private static bool InvalidCommand()
        {
            Console.WriteLine("Invalid input: Could not recognise command or this command has beeen used before");
            return false;
        }

        public bool EndOfGame()
        {
            return Data.All(x => x.Value.IsUsed);
        }

        public void Reroll()
        {
            DiceToReroll.ForEach(x => x.Roll());
        }
    }
}
