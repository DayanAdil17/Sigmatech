using AutoMapper;
using src.DTO.Transaction.Request;
using src.DTO.Transaction.Response;
using src.Entities.Transaction;

namespace src.Mapper.Transaction
{
    public class TransactionConfigurationProfile : Profile
    {
        public TransactionConfigurationProfile()
        {
            CreateMap<CreateTransactionRequest, TransactionEntity>()
                .ForMember(a => a.detailTransactions, b => b.MapFrom(src => src.detailTransaction));
            CreateMap<DetailTransactionRequest, DetailTransaction>();

            CreateMap<TransactionEntity, CreateTransactionResponse>()
                .ForMember(a => a.detailTransaction, b => b.MapFrom(src => src.detailTransactions));
            CreateMap<DetailTransaction, DetailTransactionResponse>();
            CreateMap<TransactionEntity, ListTransactionResponse>()
                .ForMember(a => a.detailTransaction, b => b.MapFrom(src => src.detailTransactions));

            CreateMap<DetailTransactionUpdate, DetailTransaction>();
            CreateMap<TransactionEntity, UpdateTransactionResponse>()
                .ForMember(a => a.detailTransaction, b => b.MapFrom(src => src.detailTransactions));
            CreateMap<DetailTransaction, DetailTransactionUpdateResponse>();
        }
    }
}