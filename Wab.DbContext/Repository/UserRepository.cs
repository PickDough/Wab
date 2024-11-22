using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wab.Core.Domain;
using Wab.Core.Repository;
using Wab.DbContext.Entity;

namespace Wab.DbContext.Repository;

public class UserRepository : IUserRepository
{
    private readonly Mapper _mapper;
    private readonly DbSet<UserEntity> _set;

    public UserRepository(Mapper mapper, DbSet<UserEntity> set)
    {
        _mapper = mapper;
        _set = set;
    }

    public User? GetById(Guid id)
    {
        return _mapper.Map<UserEntity?, User>(_set
            .Include(u => u.Cards).Include(u => u.PaymentDue)
            .FirstOrDefault(u => u.Id == id));
    }
}