using Microsoft.AspNetCore.Mvc;
using Wab.Core.Domain;
using Wab.Core.Dto;
using Wab.Core.Service;

namespace Wab.Api.Controllers;

[ApiController, Route("api/user/{userId:guid}/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly TransactionService _transactionService;

    public TransactionsController(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet("{id:guid}")]
    public ActionResult<Transaction> GetById(Guid id, Guid userId)
    {
        var transaction = _transactionService.GetById(id, userId);
        return Ok(transaction);
    }


    [HttpGet("card/{cardId:guid}")]
    public ActionResult<IEnumerable<TransactionDto>> GetByCardId(Guid cardId, Guid userId, int page = 1,
        int pageSize = 10)
    {
        var transactions = _transactionService.GetByCardId(cardId, userId, page, pageSize);
        return Ok(transactions);
    }
}