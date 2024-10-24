namespace src.DTO.Transaction.Request
{
    public class CreateTransactionRequest
    {
        public List<DetailTransactionRequest> detailTransaction { get; set; }
    }

    public class DetailTransactionRequest
    {
        public string menuName { get; set; }
        public string menuCode { get; set; }
        public string menuType { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
    }
}