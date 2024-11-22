using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Wab.Core.Domain;

namespace Wab.DbContext.Entity;

[Index(nameof(Date))]
public class TransactionEntity
{
    public Guid Id { get; set; }
    public Guid CardId { get; set; }
    public CardEntity? Card { get; set; }
    public TransactionType Type { get; set; }
    [Column(TypeName = "money")] public decimal Amount { get; set; }
    public required string Name { get; set; }
    public DateTime Date { get; set; }
    public TransactionStatus Status { get; set; }
    public string? Description { get; set; }
    public Guid AuthorizedUserId { get; set; }
    public UserEntity? AuthorizedUser { get; set; }
}