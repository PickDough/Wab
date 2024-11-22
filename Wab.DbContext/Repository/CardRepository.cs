using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wab.Core.Domain;
using Wab.Core.Repository;
using Wab.DbContext.Entity;

namespace Wab.DbContext.Repository;

public class CardRepository : ICardRepository
{
    private readonly Mapper _mapper;
    private readonly DbSet<CardEntity> _set;

    public CardRepository(Mapper mapper, DbSet<CardEntity> set)
    {
        _mapper = mapper;
        _set = set;
    }

    public Card? GetById(Guid id)
    {
        return _mapper.Map<CardEntity?, Card?>(_set.FirstOrDefault(c => c.Id == id));
    }

    public Card? GetUserPrimaryCard(Guid userId)
    {
        return _mapper.Map<CardEntity?, Card>(_set.MinBy(c => c.DateObtained));
    }
}