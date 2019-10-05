using System.Collections.Generic;

namespace Yahtzee.ScoringCategories
{
    public class Yahtzee : IScoringCategories
    {
        public Yahtzee()
        {
            Name = nameof(Yahtzee);
        }

        public Score CountPoints(List<Die> dice)
        {
            var count = new int[6];
            var value = 0;
            foreach (var die in dice) count[die.RollValue - 1]++;
            for (var i = count.Length - 1; i >= 0; i--)
            {
                if (count[i] < 5) continue;
                value = 50;
                break;
            }

            return new Score
            {
                Value = value
            };
        }

        public string Name { get; set; }

        public static bool ValidateIfYahtzee(List<Die> dice)
        {
            var count = new int[6];
            foreach (var die in dice) count[die.RollValue - 1]++;
            for (var i = count.Length - 1; i >= 0; i--)
            {
                if (count[i] < 5) continue;
                return true;
            }

            return false;
        }
    }
}