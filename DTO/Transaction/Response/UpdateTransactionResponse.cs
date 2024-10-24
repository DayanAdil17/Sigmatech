using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.DTO.Transaction.Response
{
    public class UpdateTransactionResponse
    {
        public Guid id { get; set; }
        public string transactionNumber { get; set; }
        public int totalPrice { get; set; }
        public bool isActive { get; set; }
        public List<DetailTransactionUpdateResponse> detailTransaction { get; set; }    
    }

    public class DetailTransactionUpdateResponse
    {
        public string menuName { get; set; }
        public string menuCode { get; set; }
        public string menuType { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
    }
}