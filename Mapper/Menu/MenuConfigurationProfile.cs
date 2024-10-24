using AutoMapper;
using src.DTO.Menu.Request;
using src.DTO.Menu.Response;
using src.Entities.Menu;

namespace src.Mapper.Menu
{
    public class MenuConfigurationProfile : Profile
    {
        public MenuConfigurationProfile()
        {
            CreateMap<MenuEntity, MenuResponse>();
            CreateMap<CreateMenuRequest, MenuEntity>();
            CreateMap<UpdateMenuRequest, MenuEntity>();
        }
    }
}