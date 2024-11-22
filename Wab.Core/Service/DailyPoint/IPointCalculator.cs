namespace Wab.Core.Service.DailyPoint;

public interface IPointCalculator
{
    public long Calculate(DateTime cardObtainedDate);
}