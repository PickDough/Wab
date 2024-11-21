using System.ComponentModel.DataAnnotations.Schema;

namespace Wab.DbContext.Entity;

[Table("PaymentDue")]
public class PaymentDueEntity
{
    public Guid Id { get; set; }
    public Guid CardId { get; set; }
    public CardEntity? Card { get; set; }
    [Column(TypeName = "date")] public DateTime Month { get; set; }
}