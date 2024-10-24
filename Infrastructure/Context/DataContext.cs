using Microsoft.EntityFrameworkCore;
using src.Entities.Menu;
using src.Entities.Transaction;
using src.Entities.User;
using src.Infrastructure.Configuration;

namespace Sigmatech.Infrastructure.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TransactionEntityConfiguration());
            base.OnModelCreating(builder);
        }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<TransactionEntity> Transactions { get; set; }
        public virtual DbSet<MenuEntity> Menus { get; set; }
    }
}