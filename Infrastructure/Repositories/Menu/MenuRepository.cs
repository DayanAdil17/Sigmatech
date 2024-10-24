using Sigmatech.Infrastructure.Context;
using Sigmatech.Infrastructure.Repositories;
using src.Entities.Menu;
using src.Interfaces.Repositories.Menu;

namespace src.Infrastructure.Repositories.Menu
{
    public class MenuRepository : Repository<MenuEntity>, IMenuRepository
    {
        public MenuRepository(DataContext context) : base(context)
        {

        }

        public MenuRepository(DataContext context, ILogger logger) : this(context)
        {

        }
    }
}