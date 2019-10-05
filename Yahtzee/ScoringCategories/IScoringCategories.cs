using System.Collections.Generic;

namespace Yahtzee.ScoringCategories
{
    public interface IScoringCategories
    {
        string Name { get; set; }
        Score CountPoints(List<Die> dice);
    }
}