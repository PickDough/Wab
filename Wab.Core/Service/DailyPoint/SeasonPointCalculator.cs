namespace Wab.Core.Service.DailyPoint;

public class SeasonPointCalculator : IPointCalculator
{
    public static long FirstDayPoints = 2;
    public static long SecondDayPoints = 3;
    private readonly Func<DateTime> _now;

    public SeasonPointCalculator(Func<DateTime> now)
    {
        _now = now;
    }

    public long Calculate(DateTime cardObtainedDate)
    {
        var today = _now();
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

    private static long CalculatePoints(int dayInSeason)
    {
        switch (dayInSeason)
        {
            case 1:
                return FirstDayPoints;
            case 2:
                return FirstDayPoints + SecondDayPoints;
        }

        var lastThreeDays = new LinkedList<long>();
        lastThreeDays.AddLast(FirstDayPoints);
        lastThreeDays.AddLast(FirstDayPoints + SecondDayPoints);

        for (var i = 2; i < dayInSeason; i++)
        {
            lastThreeDays.AddLast(lastThreeDays.First!.Next!.Value + (lastThreeDays.First!.Value +
                                                                      lastThreeDays.First!.Next!.Value * 6 / 10));
            lastThreeDays.RemoveFirst();
        }

        return lastThreeDays.Last!.Value;
    }
}