using src.Interfaces.Repositories.Menu;
using src.Interfaces.Repositories.Transaction;
using src.Interfaces.Repositories.User;

namespace Sigmatech.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {         
        IUserRepository userRepository { get; }
        ITransactionRepository transactionRepository { get; }
        IMenuRepository menuRepository { get; }

         Task<int>CommitAsync();
    }
}