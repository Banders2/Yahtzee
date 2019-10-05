using System.Collections.Generic;
using System.Linq;

namespace Yahtzee.ScoringCategories
{
    public class Chance : IScoringCategories
    {
        public Chance()
        {
            Name = nameof(Chance);
        }

        public Score CountPoints(List<Die> dice)
        {
            return new Score
            {
                Value = dice.Sum(die => die.RollValue)
            };
        }

        public string Name { get; set; }
    }
}