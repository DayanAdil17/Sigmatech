using Sigmatech.Infrastructure.Context;
using Sigmatech.Infrastructure.Repositories;
using src.Entities.User;
using src.Interfaces.Repositories.User;

namespace src.Infrastructure.Repositories.User
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {

        }

        public UserRepository(DataContext context, ILogger logger) : this(context)
        {

        }
    }
}