using System.Numerics;

namespace Wab.Core.Domain;

public class UserCompound
{
    public UserCompound(User user, Card primaryCard, BigInteger dailyPoints)
    {
        User = user;
        PrimaryCard = primaryCard;
        DailyPoints = dailyPoints;
    }

    public User User { get; }
    public Card PrimaryCard { get; }
    public BigInteger DailyPoints { get; }
}