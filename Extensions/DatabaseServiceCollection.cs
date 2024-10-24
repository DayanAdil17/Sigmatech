using Microsoft.EntityFrameworkCore;
using Sigmatech.Infrastructure.Context;

namespace Sigmatech.Extensions;

public static class DatabaseServiceCollection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        // Use GetConnectionString to fetch the connection string from appsettings.json
        return services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));  // The connection string
    }
}
