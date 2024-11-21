using Wab.Core.Domain;

namespace Wab.Core.Repository;

public interface IUserRepository
{
    User? GetById(Guid id);
}