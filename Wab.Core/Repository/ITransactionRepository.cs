using Wab.Core.Domain;

namespace Wab.Core.Repository;

public interface ITransactionRepository
{
    Transaction? GetById(Guid id);
    IEnumerable<Transaction> GetByCardId(Guid cardId, int page, int pageSize);
}