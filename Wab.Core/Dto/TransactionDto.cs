using Wab.Core.Domain;

namespace Wab.Core.Dto;

public class TransactionDto
{
    public TransactionDto(Transaction transaction)
    {
        Id = transaction.Id;
        CardId = transaction.Card.Id;
        Type = transaction.Type;
        Amount = transaction.Amount;
        Name = transaction.Name;
        Date = transaction.Date;
        Status = transaction.Status;
        AuthorizedUserId = transaction.AuthorizedUser.Id;
    }

    public TransactionDto(
        Guid id, Guid cardId,
        TransactionType type,
        decimal amount,
        string name,
        DateTime date,
        TransactionStatus status, Guid authorizedUserId)
    {
        Id = id;
        CardId = cardId;
        Type = type;
        Amount = amount;
        Name = name;
        Date = date;
        Status = status;
        AuthorizedUserId = authorizedUserId;
    }

    public Guid Id { get; }
    public Guid CardId { get; }
    public TransactionType Type { get; }
    public decimal Amount { get; }
    public string Name { get; }
    public DateTime Date { get; }
    public TransactionStatus Status { get; }
    public Guid AuthorizedUserId { get; }
    public string? Description { get; init; }
}