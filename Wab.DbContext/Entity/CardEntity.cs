using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wab.Core.Domain;

namespace Wab.DbContext.Entity;

[Table("Card")]
public class CardEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserEntity? User { get; set; }

    [Range(Card.MinLimit, Card.MaxLimit), Column(TypeName = "money")]
    public decimal Balance { get; set; }

    public DateTime DateObtained { get; set; }
}