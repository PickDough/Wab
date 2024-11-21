namespace Wab.Core.Domain;

public class PaymentDue
{
    public PaymentDue(Guid id, Guid cardId, DateTime month)
    {
        Id = id;
        CardId = cardId;
        Month = month;
    }

    public Guid Id { get; }
    public Guid CardId { get; }
    public DateTime Month { get; }
}