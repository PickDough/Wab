namespace Wab.Core.Domain;

public class User
{
    public User(Guid id, string firstName, string lastName, PaymentDue paymentDue, IEnumerable<Card> cards)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Cards = cards;
        PaymentDue = paymentDue;
    }

    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public PaymentDue PaymentDue { get; }
    public IEnumerable<Card> Cards { get; }
}