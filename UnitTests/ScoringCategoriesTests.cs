using System;
using System.Collections.Generic;
using Xunit;
using Yahtzee;
using Yahtzee.ScoringCategories;

namespace UnitTests
{
    public class ScoringCategoriesTests
    {
        //[Fact]
        //public void StraightTest()
        //{
        //    var straight = new Straight(4, "Straight", 25);

        //    var list = new List<Die>
        //    {
        //        new Die(1, 1),
        //        new Die(2, 2),
        //        new Die(3, 3),
        //        new Die(4, 4),
        //        new Die(5, 5)
        //    };
        //    list.ForEach(die => die.Roll());

        //    var result = straight.CountPoints(list);


        //    Assert.Equal(25, result);
        //}

        [Theory]
        [InlineData(1, 2, 3, 4, 5)]
        [InlineData(2, 2, 3, 4, 5)]
        [InlineData(1, 2, 3, 4, 6)]
        [InlineData(1, 2, 3, 3, 4)]
        public void Straight4TestGivePoints(int die1, int die2, int die3, int die4, int die5)
        {
            const int points = 25;
            var straight = new Straight(4, "Straight", points);

            var list = new List<Die>
            {
                new Die(die1, die1),
                new Die(die2, die2),
                new Die(die3, die3),
                new Die(die4, die4),
                new Die(die5, die5)
            };
            list.ForEach(die => die.Roll());
            var result = straight.CountPoints(list);

            Assert.Equal(points, result);
        }

        [Theory]
        [InlineData(1, 2, 6, 6, 5)]
        [InlineData(2, 2, 3, 1, 5)]
        [InlineData(1, 2, 3, 6, 6)]
        [InlineData(1, 1, 3, 3, 4)]
        public void Straight4TestNoPoints(int die1, int die2, int die3, int die4, int die5)
        {
            const int points = 25;
            var straight = new Straight(4, "Straight", points);

            var list = new List<Die>
            {
                new Die(die1, die1),
                new Die(die2, die2),
                new Die(die3, die3),
                new Die(die4, die4),
                new Die(die5, die5)
            };
            list.ForEach(die => die.Roll());
            var result = straight.CountPoints(list);

            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData(1, 2, 3, 4, 5)]
        public void Straight5TestGivePoints(int die1, int die2, int die3, int die4, int die5)
        {
            const int points = 25;
            var straight = new Straight(5, "Straight", points);

            var list = new List<Die>
            {
                new Die(die1, die1),
                new Die(die2, die2),
                new Die(die3, die3),
                new Die(die4, die4),
                new Die(die5, die5)
            };
            list.ForEach(die => die.Roll());
            var result = straight.CountPoints(list);

            Assert.Equal(points, result);
        }

        [Theory]
        [InlineData(1, 2, 3, 4, 6)]
        [InlineData(2, 3, 4, 5, 5)]
        [InlineData(1, 2, 4, 5, 6)]
        public void Straight5TestNoPoints(int die1, int die2, int die3, int die4, int die5)
        {
            const int points = 25;
            var straight = new Straight(5, "Straight", points);

            var list = new List<Die>
            {
                new Die(die1, die1),
                new Die(die2, die2),
                new Die(die3, die3),
                new Die(die4, die4),
                new Die(die5, die5)
            };
            list.ForEach(die => die.Roll());
            var result = straight.CountPoints(list);

            Assert.Equal(0, result);
        }
    }
}
