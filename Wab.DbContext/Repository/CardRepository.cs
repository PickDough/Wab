using AutoMapper;
using Wab.Core.Domain;
using Wab.Core.Repository;
using Wab.DbContext.Entity;

namespace Wab.DbContext.Repository;

public class CardRepository : ICardRepository
{
    private readonly WabDbContext _db;
    private readonly IMapper _mapper;

    public CardRepository(IMapper mapper, WabDbContext db)
    {
        _mapper = mapper;
        _db = db;
    }

    public Card? GetById(Guid id)
    {
        return _mapper.Map<CardEntity?, Card?>(_db.Cards.FirstOrDefault(c => c.Id == id));
    }

    public Card? GetUserPrimaryCard(Guid userId)
    {
        var cardEntities = from card in _db.Cards
            join user in _db.Users
                on card.UserId equals user.Id
            where user.Id == userId
            orderby card.DateObtained
            select card;
        return _mapper.Map<CardEntity?, Card>(cardEntities.FirstOrDefault());
    }
}