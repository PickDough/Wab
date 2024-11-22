using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wab.DbContext.Entity;

[Table("User")]
public class UserEntity
{
    public Guid Id { get; set; }
    [MaxLength(256)] public string FirstName { get; set; } = "";
    [MaxLength(256)] public string LastName { get; set; } = "";
    public IEnumerable<CardEntity> Cards { get; set; } = new List<CardEntity>();
    public PaymentDueEntity? PaymentDue { get; set; }
}