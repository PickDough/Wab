using Wab.Core.Domain;
using Wab.Core.Domain.Exception;
using Wab.Core.Repository;

namespace Wab.Core.Service;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User GetById(Guid id)
    {
        var user = _userRepository.GetById(id);
        if (user is null)
            throw new NotFoundException(id, nameof(User));

        return user;
    }
}