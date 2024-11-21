using Wab.Core.Domain;
using Wab.Core.Domain.Exception;
using Wab.Core.Repository;

namespace Wab.Core.Service;

public class CardService
{
    private readonly ICardRepository _cardRepository;

    public CardService(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public Card GetById(Guid id, Guid userId)
    {
        var card = _cardRepository.GetById(id);
        if (card is null)
            throw new NotFoundException(id, nameof(Card));

        if (!card.IsAuthorized(userId))
            throw new UnauthorizedException(userId, nameof(Card));

        return card;
    }

    public Card GetUserPrimaryCard(Guid userId)
    {
        var primaryCard = _cardRepository.GetUserPrimaryCard(userId);
        if (primaryCard is null)
            throw new NotFoundException(userId, nameof(Card));

        return primaryCard;
    }
}