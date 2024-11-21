using Wab.Core.Domain;

namespace Wab.Core.Repository;

public interface ICardRepository
{
    Card? GetById(Guid id);
    Card? GetUserPrimaryCard(Guid userId);
}