using System.ComponentModel.Design;
using Sigmatech.Infrastructure.UnitOfWork;
using Sigmatech.Interfaces.UnitOfWork;

using Sigmatech.Settings;
using src.Services.Login.Query;
using src.Services.Menu.Query;
using src.Services.Transaction.Command;
using src.Services.Transaction.Query;
using src.Services.User.Query;


namespace Sigmatech.Extensions
{
    public static class ServicesServiceCollection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            MailSettings mailSettings = configuration.GetSection("MailSettings").Get<MailSettings>();
            Console.WriteLine($"mail settings {mailSettings}");
            return services
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<LoginQuery>()
                .AddTransient<MenuQuery>()
                .AddTransient<TransactionCommand>()
                .AddTransient<TransactionQuery>()
                .AddTransient<UserInfoQuery>()
                .AddTransient<MenuCommand>();
        }
    }
}