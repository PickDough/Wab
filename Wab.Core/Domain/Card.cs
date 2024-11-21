namespace Wab.Core.Domain;

public class Card
{
    public const int MinLimit = 0;
    public const int MaxLimit = 1500;

    public Card(Guid id, Guid userId, decimal balance, DateTime dateObtained)
    {
        Id = id;
        UserId = userId;
        Balance = balance;
        DateObtained = dateObtained;
    }

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Balance { get; set; }
    public DateTime DateObtained { get; set; }

    public bool IsAuthorized(Guid userId)
    {
        return UserId == userId;
    }
}