using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using src.Entities.Transaction;

namespace src.Infrastructure.Configuration
{
    public class TransactionEntityConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.OwnsMany(
                e => e.detailTransactions,
                ownedNavigationBuilder =>
                {
                    ownedNavigationBuilder.ToJson();
                }
            );
        }
    }
}