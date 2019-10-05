using System;
using System.Collections.Generic;
using System.Linq;
using Yahtzee.ScoringCategories;

namespace Yahtzee
{
    public class Yahtzee
    {
        private const int DieStart = 1;
        private const int DieEnd = 6;
        private const string RollStringStart = "roll ";
        private readonly List<Die> _dice = new List<Die>();
        private readonly List<IScoringCategories> _scoringCategories = new List<IScoringCategories>();
        private int _bonus;
        private Score _lowerScoreBracket;
        private Score _upperScoreBracket;
        private bool _upperScoreBracketBonusDone;
        private bool _yahtzeeDone;


        public Yahtzee(int dice)
        {
            for (var j = 0; j < dice; j++) _dice.Add(new Die(DieStart, DieEnd));
            AddCategories();
            _upperScoreBracket = new Score(Bracket.Upper);
            _lowerScoreBracket = new Score();
        }

        private void AddCategories()
        {
            _scoringCategories.Add(new SameNumbers(1, "Aces"));
            _scoringCategories.Add(new SameNumbers(2, "Twos"));
            _scoringCategories.Add(new SameNumbers(3, "Threes"));
            _scoringCategories.Add(new SameNumbers(4, "Fours"));
            _scoringCategories.Add(new SameNumbers(5, "Fives"));
            _scoringCategories.Add(new SameNumbers(6, "Sixes"));
            _scoringCategories.Add(new XOfAKind(3, "3 Of A Kind"));
            _scoringCategories.Add(new XOfAKind(4, "4 Of A Kind"));
            _scoringCategories.Add(new FullHouse());
            _scoringCategories.Add(new Straight(4, "Small Straight", 30));
            _scoringCategories.Add(new Straight(5, "Large Straight", 40));
            _scoringCategories.Add(new Chance());
            _scoringCategories.Add(new ScoringCategories.Yahtzee());
        }

        public void HowToPlay()
        {
            Console.WriteLine("This is Yahtzee");
            Console.WriteLine("Each round you roll 5 dice and then it is possible to choose one " +
                              "of the categories by writing the number in the terminal. To choose Aces in the first" +
                              " round write [0] without the square brackets. If die 1, 2, and 4 should be rerolled," +
                              " then write [roll 124] without the square brackets. Press any key to start!");
        }

        public bool Round()
        {
            _dice.ForEach(die => die.Roll());
            Print();

            var diceRolls = 0;
            while (true)
            {
                if (diceRolls >= 3) Console.WriteLine("No more rolls");
                var choice = Console.ReadLine();

                if (diceRolls < 3 && choice != null && choice.StartsWith(RollStringStart))
                {
                    var rollString = choice.Substring(RollStringStart.Length);
                    if (!ValidateRollString(rollString)) continue;
                    foreach (var i in rollString.Select(c => c - '0')) _dice[i - 1].Roll();
                    diceRolls++;
                    Print();
                }

                if (ChooseCategory(choice))
                    break;
            }

            if (_scoringCategories.Any()) return true;

            var sum = 0;
            sum += _bonus;
            sum += _upperScoreBracket.Value;
            sum += _lowerScoreBracket.Value;

            Console.WriteLine($"Game over you scored {sum}");
            return false;
        }

        private void Print()
        {
            Console.Clear();
            Console.WriteLine(
                $"Current Score: {_lowerScoreBracket.Value + _upperScoreBracket.Value + _bonus}\n{_lowerScoreBracket.Bracket}: {_lowerScoreBracket.Value}\n{_upperScoreBracket.Bracket}: {_upperScoreBracket.Value}\nBonus: {_bonus}");
            Console.WriteLine();
            _dice.ForEach(die => die.PrintRoll());
            Console.WriteLine();
            Console.WriteLine();
            PrintScoreForEachCategory();
            Console.WriteLine("Now roll again using [roll 124] or choose category like [1] with no square brackets");
        }

        private static bool ValidateRollString(string rollString)
        {
            return rollString.Length <= 5 && rollString.All(char.IsDigit) &&
                   string.Concat(rollString.Distinct()) == rollString;
        }

        private bool ChooseCategory(string choice)
        {
            if (!int.TryParse(choice, out var choiceNumber)) return false;
            if (choiceNumber < 0 || choiceNumber >= _scoringCategories.Count) return false;

            if (_yahtzeeDone && ScoringCategories.Yahtzee.ValidateIfYahtzee(_dice))
            {
                Console.WriteLine("BONUS!!! 100 for second Yahtzee");
                _bonus += 100;
            }

            var countPoints = _scoringCategories[choiceNumber].CountPoints(_dice);

            if (_scoringCategories[choiceNumber] is ScoringCategories.Yahtzee && countPoints.Value != 0)
                _yahtzeeDone = true;


            Score.TryAdd(ref _upperScoreBracket, countPoints);
            if (!_upperScoreBracketBonusDone && _upperScoreBracket.Value >= 63)
            {
                _upperScoreBracketBonusDone = true;
                _bonus += 35;
            }

            Score.TryAdd(ref _lowerScoreBracket, countPoints);

            _scoringCategories.RemoveAt(choiceNumber);
            return true;
        }

        public void PrintScoreForEachCategory()
        {
            for (var index = 0; index < _scoringCategories.Count; index++)
            {
                var cat = _scoringCategories[index];
                Console.WriteLine($"[{index}] {cat.Name} can give {cat.CountPoints(_dice).Value} with this roll");
            }
        }
    }
}