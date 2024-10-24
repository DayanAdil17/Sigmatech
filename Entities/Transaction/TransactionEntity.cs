using Sigmatech.Entities.Base;

namespace src.Entities.Transaction
{
    public class TransactionEntity : BaseEntity
    {
        public string transactionNumber { get; set; }
        public int totalPrice { get; set; }
        public string pic { get; set; }
        public bool isActive { get; set; }
        public bool isPaid { get; set; }
        public List<DetailTransaction> detailTransactions { get; set; }
    }

    public class DetailTransaction
    {
        public string menuName { get; set; }
        public string menuCode { get; set; }
        public string menuType { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
    }
}