using AutoMapper;
using Sigmatech.Interfaces.UnitOfWork;
using src.DTO.Menu.Response;

namespace src.Services.Menu.Query
{
    public class MenuQuery
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public MenuQuery(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            
        }
        
        public async Task<List<MenuResponse>> GetAvailableMenu ()
        {
            var menu = await unitOfWork.menuRepository.Finds(x => x.isAvailable);

            return mapper.Map<List<MenuResponse>>(menu);
        }
    }
}