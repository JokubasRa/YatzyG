using System;


namespace Yatzy
{
    /// <summary>
    /// Class for a dice which allows basic dice operations
    /// </summary>
    public class Dice
    {
        public Dice(int Index)
        {
            if (Index < 1) { throw new ArgumentOutOfRangeException(); }
            Num = new Random().Next(1, 7);
            IndexOfDice = Index;
        }


        public int Num { get; private set; }
        public int IndexOfDice { get; }

        public void Roll()
        {
            Num = new Random().Next(1, 7);
        }
    }
}
