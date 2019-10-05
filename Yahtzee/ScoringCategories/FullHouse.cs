using System.Collections.Generic;

namespace Yahtzee.ScoringCategories
{
    internal class FullHouse : IScoringCategories
    {
        public FullHouse()
        {
            Name = nameof(FullHouse);
        }

        public Score CountPoints(List<Die> dice)
        {
            var three = false;
            var two = false;

            var count = new int[6];
            foreach (var die in dice) count[die.RollValue - 1]++;
            for (var i = count.Length - 1; i >= 0; i--)
            {
                if (count[i] == 3) three = true;
                if (count[i] == 2) two = true;
            }

            var value = 0;
            if (three && two) value = 25;

            return new Score
            {
                Value = value
            };
        }

        public string Name { get; set; }
    }
}