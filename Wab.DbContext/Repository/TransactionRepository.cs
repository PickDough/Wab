using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wab.Core.Domain;
using Wab.Core.Repository;
using Wab.DbContext.Entity;

namespace Wab.DbContext.Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly WabDbContext _db;
    private readonly IMapper _mapper;

    public TransactionRepository(IMapper mapper, WabDbContext db)
    {
        _mapper = mapper;
        _db = db;
    }

    public Transaction? GetById(Guid id)
    {
        return _mapper.Map<TransactionEntity?, Transaction?>(_db.Transactions
            .Include(t => t.Card)
            .ThenInclude(c => c!.User)
            .Include(t => t.AuthorizedUser)
            .FirstOrDefault(t => t.Id == id));
    }

    public IEnumerable<Transaction> GetByCardId(Guid cardId, int page, int pageSize)
    {
        return _mapper.Map<IEnumerable<TransactionEntity>, IEnumerable<Transaction>>(_db.Transactions
            .Include(t => t.Card)
            .ThenInclude(c => c!.User)
            .Include(t => t.AuthorizedUser)
            .Where(t => t.CardId == cardId)
            .OrderByDescending(t => t.Date)
            .Skip((page - 1) * pageSize)
            .Take(pageSize));
    }
}