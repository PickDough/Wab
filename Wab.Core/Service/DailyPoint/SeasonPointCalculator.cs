using System.Numerics;

namespace Wab.Core.Service.DailyPoint;

public class SeasonPointCalculator : IPointCalculator
{
    public BigInteger Calculate(DateTime cardObtainedDate)
    {
        var today = DateTime.Today;
        var dayInSeason = GetDayInSeason(today);
        var points = CalculatePoints(dayInSeason);
        return points;
    }

    private static int GetDayInSeason(DateTime date)
    {
        var seasonStart = GetSeasonStartDate(date);
        return (date - seasonStart).Days + 1;
    }

    private static DateTime GetSeasonStartDate(DateTime date)
    {
        return date.Month switch
        {
            >= 3 and < 6 => new DateTime(date.Year, 3, 1),
            >= 6 and < 9 => new DateTime(date.Year, 6, 1),
            >= 9 and < 12 => new DateTime(date.Year, 9, 1),
            _ => new DateTime(date.Year, 12, 1)
        };
    }

    private static BigInteger CalculatePoints(int dayInSeason)
    {
        const int dayBeforeYesterdayPoints = 2;
        if (dayInSeason == 1) return dayBeforeYesterdayPoints;
        const int yesterdayPoints = dayBeforeYesterdayPoints + 3;
        if (dayInSeason == 2) return yesterdayPoints;

        var lastThreeDays = new LinkedList<BigInteger>();
        lastThreeDays.AddLast(dayBeforeYesterdayPoints);
        lastThreeDays.AddLast(yesterdayPoints);

        for (var i = 2; i < dayInSeason; i++)
        {
            lastThreeDays.AddLast(lastThreeDays.First!.Value +
                                  (BigInteger)((float)lastThreeDays.First!.Next!.Value * 0.6));
            lastThreeDays.RemoveFirst();
        }

        return lastThreeDays.Last!.Value;
    }
}