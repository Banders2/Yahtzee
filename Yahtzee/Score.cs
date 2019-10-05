using System;

namespace Yahtzee
{
    public class Score
    {
        public Score(Bracket bracket = Bracket.Lower)
        {
            Bracket = bracket;
            Value = 0;
        }

        public Bracket Bracket { get; set; }
        public int Value { get; set; }

        public static bool TryAdd(ref Score score1, Score score2)
        {
            if (score1.Bracket != score2.Bracket) return false;
            score1.Value += score2.Value;
            return true;
        }

        public static Score operator +(Score score1, Score score2)
        {
            if (score1.Bracket != score2.Bracket)
                throw new NotSupportedException("Two types should have the same bracket to to added");
            score1.Value += score2.Value;
            return score1;
        }
    }
}