using System;

namespace Yahtzee
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var yahtzee = new Yahtzee(5);
            yahtzee.HowToPlay();
            Console.Read();
            var gameStillAlive = true;
            while (gameStillAlive) gameStillAlive = yahtzee.Round();
        }
    }
}