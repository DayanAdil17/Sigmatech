namespace src.DTO.Transaction.Response
{
    public class CreateTransactionResponse
    {
        public Guid id { get; set; }
        public string transactionNumber { get; set; }
        public int totalPrice { get; set; }
        public bool isActive { get; set; }
        public List<DetailTransactionResponse> detailTransaction { get; set; }    
    }

    public class DetailTransactionResponse
    {
        public string menuName { get; set; }
        public string menuCode { get; set; }
        public string menuType { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
    }
}