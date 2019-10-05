using System.Collections.Generic;
using System.Linq;

namespace Yahtzee.ScoringCategories
{
    internal class SameNumbers : IScoringCategories
    {
        private readonly int _number;

        public SameNumbers(int number, string categoryName)
        {
            _number = number;
            Name = categoryName;
        }

        public Score CountPoints(List<Die> dice)
        {
            return new Score
            {
                Bracket = Bracket.Upper,
                Value = dice.Where(die => die.RollValue == _number).Sum(die => die.RollValue)
            };
        }

        public string Name { get; set; }
    }
}