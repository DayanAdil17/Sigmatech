namespace src.DTO.Transaction.Response
{
    public class ListTransactionResponse
    {
        public Guid id { get; set; }
        public string transactionNumber { get; set; }
        public int totalPrice { get; set; }
        public bool isActive { get; set; }
        public List<DetailTransactionResponse> detailTransaction { get; set; }    
    }
}