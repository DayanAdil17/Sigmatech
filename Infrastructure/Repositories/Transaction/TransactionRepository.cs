using Sigmatech.Infrastructure.Context;
using Sigmatech.Infrastructure.Repositories;
using src.Entities.Transaction;
using src.Interfaces.Repositories.Transaction;

namespace src.Infrastructure.Repositories.Transaction
{
    public class TransactionRepository : Repository<TransactionEntity>, ITransactionRepository
    {
        public TransactionRepository(DataContext context) : base(context)
        {

        }

        public TransactionRepository(DataContext context, ILogger logger) : this(context)
        {

        }
    }
}