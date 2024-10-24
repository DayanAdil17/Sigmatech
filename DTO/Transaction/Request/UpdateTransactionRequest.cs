namespace src.DTO.Transaction.Request
{
    public class UpdateTransactionRequest
    {
        public List<DetailTransactionUpdate> detailTransaction { get; set; }
    }
    public class DetailTransactionUpdate
    {
        public string menuName { get; set; }
        public string menuCode { get; set; }
        public string menuType { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
    }
}