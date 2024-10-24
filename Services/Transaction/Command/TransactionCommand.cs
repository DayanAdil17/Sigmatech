using AutoMapper;
using Sigmatech.Exceptions.Global;
using Sigmatech.Interfaces.UnitOfWork;
using src.DTO.Transaction.Request;
using src.DTO.Transaction.Response;
using src.Entities.Transaction;
using src.Exceptions.Global;
using src.Services.User.Query;

namespace src.Services.Transaction.Command
{
    public class TransactionCommand
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserInfoQuery userInfoQuery;
        public TransactionCommand(IMapper mapper, IUnitOfWork unitOfWork, UserInfoQuery userInfoQuery)
        {
            this.userInfoQuery = userInfoQuery;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            
        }
        
        public async Task<CreateTransactionResponse> CreateTransaction (CreateTransactionRequest request)
        {
            var userInfo = userInfoQuery.GetUserInfoFromToken();

            if(userInfo.userRole != "PELAYAN")
            {
                throw new UnprocessableEntityException( "Close Bill","userRole","NOT-AVAILABLE", userInfo.userName);
            }

            var totalPrice = 0;

            foreach (var item in request.detailTransaction)
            {
                totalPrice = totalPrice + item.quantity * item.price;
            }

            var transactionNumber = await GenerateTransactionNumber();

            var transaction = mapper.Map<TransactionEntity>(request);

            transaction.transactionNumber = transactionNumber;
            transaction.totalPrice = totalPrice;
            transaction.pic = userInfo.userName;
            transaction.isActive = true;
            transaction.isPaid = false;

            var validation = ValidationMenu(transaction);

            unitOfWork.transactionRepository.Add(transaction);
            await unitOfWork.CommitAsync();

            return mapper.Map<CreateTransactionResponse>(transaction);
        }

        public async Task<UpdateTransactionResponse> UpdateTransaction (UpdateTransactionRequest request, string transactionNumber)
        {
            var transaction = await unitOfWork.transactionRepository.FindSingleOrDefault(x => x.transactionNumber == transactionNumber);

            if(transaction == null)
            {
                throw new NotFoundGlobalException("Transaction", "TRANSACTION", "transactionNumber", transactionNumber);
            }

            transaction.detailTransactions = mapper.Map<List<DetailTransaction>>(request.detailTransaction);

            transaction.totalPrice = CountPrice(request);

            var validation = ValidationMenu(transaction);

            unitOfWork.transactionRepository.Update(transaction);
            await unitOfWork.CommitAsync();

            return mapper.Map<UpdateTransactionResponse>(transaction);
        }

        private int CountPrice(UpdateTransactionRequest transaction)
        {
            var price = 0;
            foreach (var item in transaction.detailTransaction)
            {
                price = price + item.price * item.quantity;
            }

            return price;
        }

        private async Task<string> GenerateTransactionNumber()
        {
            DateTime currentDate = DateTime.Now;
            var date = currentDate.Day;
            var month = currentDate.Month;
            var year = currentDate.Year;
            var transaction = await unitOfWork.transactionRepository.GetAll();

            var counter = 0;

            if(!transaction.Any())
            {
                counter = 1;
            }
            else
            {
                counter = transaction.Count() + 1;
            }
            var transactionNumber = $"ABC{date}{month}{year}-{counter.ToString("D3")}";

            return transactionNumber;
        }
    
        private async Task<bool> ValidationMenu(TransactionEntity transaction)
        {
            var selectedMenu = transaction.detailTransactions.Select(x => x.menuCode);
            var menus = await unitOfWork.menuRepository.Finds(x => selectedMenu.Contains(x.menuCode));

            if(menus.Any(x => !x.isAvailable))
            {
                throw new UnprocessableEntityException("Transaction","menuName","NOT-AVAILABLE", menus.Where(x => !x.isAvailable).Select(x => x.id));
            }

            return true;
        }
    
        public async Task<CreateTransactionResponse> CloseBill (string transactionNumber)
        {
            var userInfo = userInfoQuery.GetUserInfoFromToken();
            var transaction = await unitOfWork.transactionRepository.FindSingleOrDefault(x => x.transactionNumber == transactionNumber);

            if(userInfo.userRole != "PETUGAS")
            {
                throw new UnprocessableEntityException( "Close Bill","userRole","NOT-AVAILABLE", userInfo.userName);
            }
            if(transaction == null)
            {
                throw new NotFoundGlobalException("Transaction", "TRANSACTION", "transactionNumber", transactionNumber);
            }

            transaction.isActive = false;
            transaction.isPaid = true;

            unitOfWork.transactionRepository.Update(transaction);
            await unitOfWork.CommitAsync();

            return mapper.Map<CreateTransactionResponse>(transaction);
        }
    
        public async Task<CreateTransactionResponse> DeleteTransaction (string transactionNumber)
        {
            var transaction = await unitOfWork.transactionRepository.FindSingleOrDefault(x => x.transactionNumber == transactionNumber);

            if(transaction == null)
            {
                throw new NotFoundGlobalException("Transaction", "TRANSACTION", "transactionNumber", transactionNumber);
            }

            unitOfWork.transactionRepository.HardDelete(x => x.transactionNumber == transaction.transactionNumber);
            await unitOfWork.CommitAsync();

            return mapper.Map<CreateTransactionResponse>(transaction);
        }
    }
}