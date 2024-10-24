using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.DTO.Transaction.Request;
using src.DTO.Transaction.Response;
using src.Services.Transaction.Command;
using src.Services.Transaction.Query;

namespace src.Controllers
{
    [ApiController]
    [Route("")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionCommand transactionCommand;
        private readonly TransactionQuery transactionQuery;
        public TransactionController(TransactionCommand transactionCommand, TransactionQuery transactionQuery)
        {
            this.transactionQuery = transactionQuery;
            this.transactionCommand = transactionCommand;
            
        }

        [Authorize]
        [HttpPost("transaction")]
        [ActionName("")]
        public async Task<CreateTransactionResponse> CreateTransaction (CreateTransactionRequest request)
        {
            return await transactionCommand.CreateTransaction(request);
        }

        [Authorize]
        [HttpGet("transaction")]
        [ActionName("")]
        public async Task<List<ListTransactionResponse>> ListTransaction ()
        {
            return await transactionQuery.ListTransactionActive();
        }

        [Authorize]
        [HttpPut("transaction/{transactionNumber}")]
        [ActionName("")]
        public async Task<UpdateTransactionResponse> UpdateTransaction (UpdateTransactionRequest request, string transactionNumber)
        {
            return await transactionCommand.UpdateTransaction(request, transactionNumber);
        }

        [Authorize]
        [HttpPut("transaction/close-bill/{transactionNumber}")]
        [ActionName("")]
        public async Task<CreateTransactionResponse> CloseBill (string transactionNumber)
        {
            return await transactionCommand.CloseBill(transactionNumber);
        }

        [Authorize]
        [HttpGet("transaction-pelayan")]
        [ActionName("")]
        public async Task<List<ListTransactionResponse>> ListTransactionPelayan ()
        {
            return await transactionQuery.ListTransactionPelayan();
        }

        [Authorize]
        [HttpDelete("transaction/{transactionNumber}")]
        [ActionName("")]
        public async Task<CreateTransactionResponse> DeleteTransaction (string transactionNumber)
        {
            return await transactionCommand.DeleteTransaction(transactionNumber);
        }
    }
}