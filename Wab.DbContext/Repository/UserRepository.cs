using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wab.Core.Domain;
using Wab.Core.Repository;

namespace Wab.DbContext.Repository;

public class UserRepository : IUserRepository
{
    private readonly WabDbContext _db;
    private readonly IMapper _mapper;

    public UserRepository(IMapper mapper, WabDbContext db)
    {
        _mapper = mapper;
        _db = db;
    }

    public User? GetById(Guid id)
    {
        var query = from paymentDue in _db.PaymentDues
            join card in _db.Cards
                on paymentDue.CardId equals card.Id
            join user in _db.Users
                on card.UserId equals user.Id
            where user.Id == id
            orderby card.DateObtained, paymentDue.Month descending
            select paymentDue;
        var u = _db.Users.Include(u => u.Cards).FirstOrDefault(u => u.Id == id);
        if (u == null) return null;

        return new User(u.Id, u.FirstName, u.LastName)
        {
            Cards = _mapper.Map<IEnumerable<Card>>(u.Cards),
            PaymentDue = _mapper.Map<PaymentDue>(query.FirstOrDefault())
        };
    }
}