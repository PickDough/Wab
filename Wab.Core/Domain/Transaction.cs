namespace Wab.Core.Domain;

public enum TransactionType
{
    Debit,
    Credit
}

public enum TransactionStatus
{
    Pending,
    Approved
}

public class Transaction
{
    public Transaction(
        Guid id, Card card,
        TransactionType type,
        decimal amount,
        string name,
        DateTime date,
        TransactionStatus status,
        User authorizedUser)
    {
        Id = id;
        Card = card;
        Type = type;
        Amount = amount;
        Name = name;
        Date = date;
        Status = status;
        AuthorizedUser = authorizedUser;
    }

    public Guid Id { get; }
    public Card Card { get; }
    public TransactionType Type { get; }
    public decimal Amount { get; }
    public string Name { get; }
    public DateTime Date { get; }
    public TransactionStatus Status { get; }
    public User AuthorizedUser { get; }
    public string? Description { get; init; }

    public bool IsByCardHolder => AuthorizedUser.Id == Card.UserId;

    public bool IsAuthorized(Guid userId)
    {
        return AuthorizedUser.Id == userId || Card.UserId == userId;
    }
}