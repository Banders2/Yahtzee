using System;

namespace Yahtzee
{
    public class Die
    {
        private readonly int _higherBound;
        private readonly int _lowerBound;
        private readonly Random _random;

        public Die(int lowerBound, int higherBound)
        {
            _lowerBound = lowerBound;
            _higherBound = higherBound + 1;
            _random = new Random();
        }

        public int RollValue { get; set; }


        public void Roll()
        {
            RollValue = _random.Next(_lowerBound, _higherBound);
        }

        public void PrintRoll()
        {
            Console.Write(RollValue + " ");
        }
    }
}