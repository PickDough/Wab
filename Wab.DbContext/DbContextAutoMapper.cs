using AutoMapper;
using Wab.Core.Domain;
using Wab.DbContext.Entity;

namespace Wab.DbContext;

public class DbContextAutoMapperProfile : Profile
{
    public DbContextAutoMapperProfile()
    {
        CreateMap<UserEntity, User>();
        CreateMap<CardEntity, Card>();
        CreateMap<PaymentDueEntity, PaymentDue>();
        CreateMap<TransactionEntity, Transaction>();
    }
}