using Sigmatech.Infrastructure.Repositories;
using Sigmatech.Interfaces.Repositories.IRepository;
using src.Infrastructure.Repositories.Menu;
using src.Infrastructure.Repositories.Transaction;
using src.Infrastructure.Repositories.User;
using src.Interfaces.Repositories.Menu;
using src.Interfaces.Repositories.Transaction;
using src.Interfaces.Repositories.User;


namespace Sigmatech.Extensions
{
    public static class RepositoriesServiceCollection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
            .AddTransient(typeof(IRepository<>), typeof(Repository<>))            
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<ITransactionRepository, TransactionRepository>()
            .AddTransient<IMenuRepository, MenuRepository>();
        }
    }
}