using Sigmatech.Infrastructure.Context;
using Sigmatech.Interfaces.UnitOfWork;
using src.Infrastructure.Repositories.Menu;
using src.Infrastructure.Repositories.Transaction;
using src.Infrastructure.Repositories.User;
using src.Interfaces.Repositories.Menu;
using src.Interfaces.Repositories.Transaction;
using src.Interfaces.Repositories.User;

namespace Sigmatech.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DataContext context;
        private ILogger logger;

        public IUserRepository userRepository { get; private set; }
        public ITransactionRepository transactionRepository { get; private set; }
        public IMenuRepository menuRepository { get; private set; }

        public UnitOfWork(DataContext context, ILoggerFactory loggerFactory)
        {
            this.context = context;
            this.logger = loggerFactory.CreateLogger("logs");            

            userRepository = new UserRepository(context, logger);
            transactionRepository = new TransactionRepository(context, logger);
            menuRepository = new MenuRepository(context, logger);

        }

        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await context.DisposeAsync();
        }
    }
}