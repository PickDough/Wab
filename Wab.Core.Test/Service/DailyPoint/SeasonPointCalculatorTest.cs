using System.Numerics;
using Wab.Core.Service.DailyPoint;

namespace Wab.Core.Test.Service.DailyPoint;

public class SeasonPointCalculatorTest
{
    public static TheoryData<DateTime, BigInteger> SimpleData => new()
    {
        { new DateTime(1984, 3, 1), SeasonPointCalculator.FirstDayPoints },
        { new DateTime(1984, 3, 2), SeasonPointCalculator.FirstDayPoints + SeasonPointCalculator.SecondDayPoints },

        { new DateTime(420, 6, 1), SeasonPointCalculator.FirstDayPoints },
        { new DateTime(420, 6, 2), SeasonPointCalculator.FirstDayPoints + SeasonPointCalculator.SecondDayPoints },

        { new DateTime(69, 9, 1), SeasonPointCalculator.FirstDayPoints },
        { new DateTime(69, 9, 2), SeasonPointCalculator.FirstDayPoints + SeasonPointCalculator.SecondDayPoints },

        { new DateTime(1, 12, 1), SeasonPointCalculator.FirstDayPoints },
        { new DateTime(1, 12, 2), SeasonPointCalculator.FirstDayPoints + SeasonPointCalculator.SecondDayPoints }
    };

    [Theory, MemberData(nameof(SimpleData)), MemberData(nameof(ComplexData))]
    public void Calculate_Test(DateTime date, BigInteger expected)
    {
        var seasonPointCalculator = new SeasonPointCalculator(() => date);
        Assert.StrictEqual(expected, seasonPointCalculator.Calculate(date));
    }

    public static TheoryData<DateTime, BigInteger> ComplexData()
    {
        var td = new TheoryData<DateTime, BigInteger>();
        var beforePrev = SeasonPointCalculator.FirstDayPoints;
        var prev = SeasonPointCalculator.SecondDayPoints + beforePrev;
        td.Add(new DateTime(1, 3, 3), Calc(beforePrev, prev));

        (beforePrev, prev) = (prev, Calc(beforePrev, prev));
        td.Add(new DateTime(1, 3, 4), Calc(beforePrev, prev));

        (beforePrev, prev) = (prev, Calc(beforePrev, prev));
        td.Add(new DateTime(1, 3, 5), Calc(beforePrev, prev));

        for (var i = 0; i < 10; i++) (beforePrev, prev) = (prev, Calc(beforePrev, prev));

        td.Add(new DateTime(1, 3, 5).AddDays(10), Calc(beforePrev, prev));

        for (var i = 0; i < 60; i++) (beforePrev, prev) = (prev, Calc(beforePrev, prev));

        td.Add(new DateTime(1, 3, 5).AddDays(10).AddDays(60), Calc(beforePrev, prev));

        return td;

        BigInteger Calc(BigInteger previousPoints, BigInteger currentPoints)
        {
            return currentPoints + (previousPoints + currentPoints * 6 / 10);
        }
    }
}