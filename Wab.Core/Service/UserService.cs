using Wab.Core.Domain;
using Wab.Core.Domain.Exception;
using Wab.Core.Repository;
using Wab.Core.Service.DailyPoint;

namespace Wab.Core.Service;

public class UserService
{
    private readonly CardService _cardService;
    private readonly IPointCalculator _pointCalculator;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, CardService cardService, IPointCalculator pointCalculator)
    {
        _userRepository = userRepository;
        _cardService = cardService;
        _pointCalculator = pointCalculator;
    }

    public User GetById(Guid id)
    {
        var user = _userRepository.GetById(id);
        if (user is null)
            throw new NotFoundException(id, nameof(User));

        return user;
    }

    public UserCompound GetFullInformation(Guid id)
    {
        return new UserCompound(GetById(id), _cardService.GetUserPrimaryCard(id),
            _pointCalculator.Calculate(DateTime.Now));
    }
}