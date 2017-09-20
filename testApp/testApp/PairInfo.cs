using System;

namespace testApp
{
    public class PairInfo
    {
        public int CountSmall { get; set; }
        public int CountBig { get; set; }
        public int S1 { get; private set; }
        public int S2 { get; private set; }
        public PairInfo(int current, int x)
        {
            S1 = Math.Min(current, x - current);
            S2 = Math.Max(current, x - current);
        }
        public void AddSummand(int current)
        {
            if (S1 == S2 && S1 == current)
            {
                if (CountBig == CountSmall)
                    CountSmall++;
                else
                    CountBig++;
            }
            else if (current == S1)
            {
                CountSmall++;
            }
            else if (current == S2)
            {
                CountBig++;
            }
            else
                throw new ArgumentOutOfRangeException("adding value must be equal to one of summand");
        }
    }
}
