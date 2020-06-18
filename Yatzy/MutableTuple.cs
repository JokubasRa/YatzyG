

namespace Yatzy
{
    /// <summary>
    /// Data structure which is similar to tuple but mutable
    /// </summary>
    public class MutableTuple
    {
        public MutableTuple(bool p, int num)
        {
            IsUsed = p;
            Score = num;
        }
        public MutableTuple()
        {
            IsUsed = false;
            Score = 0;
        }
        public bool IsUsed { get; set; }
        public int Score { get; set; }
    }
}
