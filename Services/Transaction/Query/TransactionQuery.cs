using AutoMapper;
using Sigmatech.Interfaces.UnitOfWork;
using src.DTO.Transaction.Response;
using src.Services.User.Query;

namespace src.Services.Transaction.Query
{
    public class TransactionQuery
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserInfoQuery userInfoQuery;
        public TransactionQuery(IMapper mapper, IUnitOfWork unitOfWork, UserInfoQuery userInfoQuery)
        {
            this.userInfoQuery = userInfoQuery;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            
        }

        public async Task<List<ListTransactionResponse>> ListTransactionActive()
        {
            var transaction = await unitOfWork.transactionRepository.Finds(x => x.isActive == true);

            return mapper.Map<List<ListTransactionResponse>>(transaction);
        }

        public async Task<List<ListTransactionResponse>> ListTransactionPelayan()
        {
            var userInfo = userInfoQuery.GetUserInfoFromToken();

            var transaction = await unitOfWork.transactionRepository.Finds(x => x.pic == userInfo.userName);

            return mapper.Map<List<ListTransactionResponse>>(transaction);
        }
    }
}