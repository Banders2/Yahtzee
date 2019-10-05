using System.Collections.Generic;
using System.Linq;

namespace Yahtzee.ScoringCategories
{
    public class Straight : IScoringCategories
    {
        private readonly int _inARow;
        private readonly int _points;

        public Straight(int inARow, string categoryName, int points)
        {
            _inARow = inARow;
            _points = points;
            Name = categoryName;
        }

        public Score CountPoints(List<Die> dice)
        {
            var list = dice.OrderBy(die => die.RollValue).ToList();
            var last = int.MinValue;
            var count = 1;
            foreach (var die in list)
            {
                if (last + 1 == die.RollValue) count++;
                last = die.RollValue;
            }

            return new Score
            {
                Value = count >= _inARow ? _points : 0
            };
        }

        public string Name { get; set; }
    }
}