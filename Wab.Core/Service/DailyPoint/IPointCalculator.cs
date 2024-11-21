using System.Numerics;

namespace Wab.Core.Service.DailyPoint;

public interface IPointCalculator
{
    public BigInteger Calculate(DateTime cardObtainedDate);
}