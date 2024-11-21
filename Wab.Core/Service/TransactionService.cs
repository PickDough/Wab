using Wab.Core.Domain;
using Wab.Core.Domain.Exception;
using Wab.Core.Repository;

namespace Wab.Core.Service;

public class TransactionService
{
    private readonly CardService _cardService;
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository, CardService cardService)
    {
        _transactionRepository = transactionRepository;
        _cardService = cardService;
    }

    public Transaction GetById(Guid id, Guid userId)
    {
        var transaction = _transactionRepository.GetById(id);
        if (transaction is null)
            throw new NotFoundException(id, nameof(Transaction));

        if (!transaction.IsAuthorized(userId))
            throw new UnauthorizedException(userId, nameof(Transaction));

        return transaction;
    }

    public IEnumerable<Transaction> GetByCardId(Guid cardId, Guid userId, int page = 1, int pageSize = 10)
    {
        var card = _cardService.GetById(cardId, userId);
        var transactions = _transactionRepository.GetByCardId(card.Id, page, pageSize);

        return transactions;
    }
}