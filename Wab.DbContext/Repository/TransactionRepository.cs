using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wab.Core.Domain;
using Wab.Core.Repository;
using Wab.DbContext.Entity;

namespace Wab.DbContext.Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly Mapper _mapper;
    private readonly DbSet<TransactionEntity> _set;

    public TransactionRepository(Mapper mapper, DbSet<TransactionEntity> set)
    {
        _mapper = mapper;
        _set = set;
    }

    public Transaction? GetById(Guid id)
    {
        return _mapper.Map<TransactionEntity?, Transaction?>(_set
            .Include(t => t.Card)
            .ThenInclude(c => c!.User)
            .Include(t => t.AuthorizedUser)
            .FirstOrDefault(t => t.Id == id));
    }

    public IEnumerable<Transaction> GetByCardId(Guid cardId, int page, int pageSize)
    {
        return _mapper.Map<IEnumerable<TransactionEntity>, IEnumerable<Transaction>>(_set
            .Include(t => t.Card)
            .ThenInclude(c => c!.User)
            .Include(t => t.AuthorizedUser)
            .Where(t => t.CardId == cardId)
            .OrderByDescending(t => t.Date)
            .Skip((page - 1) * pageSize)
            .Take(pageSize));
    }
}