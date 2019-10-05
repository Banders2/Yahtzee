using System.Collections.Generic;
using System.Linq;

namespace Yahtzee.ScoringCategories
{
    public class XOfAKind : IScoringCategories
    {
        private readonly int _x;

        public XOfAKind(int x, string categoryName)
        {
            _x = x;
            Name = categoryName;
        }

        public Score CountPoints(List<Die> dice)
        {
            var count = new int[6];
            var value = 0;
            foreach (var die in dice) count[die.RollValue - 1]++;
            for (var i = count.Length - 1; i >= 0; i--)
            {
                if (count[i] < _x) continue;
                value = dice.Sum(die => die.RollValue);
                break;
            }

            return new Score
            {
                Value = value
            };
        }

        public string Name { get; set; }
    }
}