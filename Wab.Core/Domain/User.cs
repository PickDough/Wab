namespace Wab.Core.Domain;

public class User
{
    public User(Guid id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public required PaymentDue PaymentDue { get; init; }
    public required IEnumerable<Card> Cards { get; init; }
}