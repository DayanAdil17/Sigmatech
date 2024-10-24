using AutoMapper;
using Sigmatech.Exceptions.Global;
using Sigmatech.Interfaces.UnitOfWork;
using src.DTO.Menu.Request;
using src.DTO.Menu.Response;
using src.Entities.Menu;
using src.Exceptions.Global;

namespace src.Services.Menu.Command
{
    public class MenuCommand
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public MenuCommand(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            
        }

        public async Task<MenuResponse> CreatMenu (CreateMenuRequest request)
        {
            var menuData = await unitOfWork.menuRepository.FindSingleOrDefault(x => x.menuCode == request.menuCode);

            if(menuData != null)
            {
                throw new AlreadyExistException("MENU", "menuCode", request.menuCode);
            }

            var menu = mapper.Map<MenuEntity>(request);
            
            menu.isAvailable = true;

            unitOfWork.menuRepository.Add(menu);
            await unitOfWork.CommitAsync();

            return mapper.Map<MenuResponse>(menu);
        }

        public async Task<MenuResponse> UpdateMenu (UpdateMenuRequest request, string menuCode)
        {
            var menu = await unitOfWork.menuRepository.FindSingleOrDefault(x => x.menuCode == menuCode);

            if(menu == null)
            {
                throw new NotFoundGlobalException("Menu", "MENU", "menuCode", menuCode);
            }

            var existingMenuCode = await unitOfWork.menuRepository.FindSingleOrDefault(x => x.menuCode == request.menuCode && x.id != menu.id);
            if(existingMenuCode != null)
            {
                throw new AlreadyExistException("MENU", "menuCode", request.menuCode);
            }

            menu = MapUpdateMenu(menu, request);

            unitOfWork.menuRepository.Update(menu);
            await unitOfWork.CommitAsync();

            return mapper.Map<MenuResponse>(menu);
        }

        public async Task<MenuResponse> DeleteMenu (string menuCode)
        {
            var menu = await unitOfWork.menuRepository.FindSingleOrDefault(x => x.menuCode == menuCode);

            if(menu == null)
            {
                throw new NotFoundGlobalException("Menu", "MENU", "menuCode", menuCode);
            }

            unitOfWork.menuRepository.HardDelete(x => x.menuCode == menu.menuCode);
            await unitOfWork.CommitAsync();

            return mapper.Map<MenuResponse>(menu);
        }

        private MenuEntity MapUpdateMenu(MenuEntity menu, UpdateMenuRequest request)
        {
            menu.menuName = request.menuName;
            menu.menuCode = request.menuCode;
            menu.menuType = request.menuType;
            menu.isAvailable = request.isAvailable;
            menu.price = request.price;

            return menu;
        }
    }
}