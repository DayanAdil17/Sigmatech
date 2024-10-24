using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.DTO.Menu.Request;
using src.DTO.Menu.Response;
using src.Services.Menu.Command;
using src.Services.Menu.Query;

namespace src.Controllers
{
    [ApiController]
    [Route("")]
    public class MenuController : ControllerBase
    {
        private readonly MenuCommand menuCommand;
        private readonly MenuQuery menuQuery;
        public MenuController(MenuQuery menuQuery, MenuCommand menuCommand)
        {
            this.menuQuery = menuQuery;
            this.menuCommand = menuCommand;
            
        }

        [Authorize]
        [HttpGet("menu")]
        [ActionName("")]
        public async Task<List<MenuResponse>> GetAvailableMenu ()
        {
            return await menuQuery.GetAvailableMenu();
        }
        [Authorize]
        [HttpPost("menu")]
        [ActionName("")]
        public async Task<MenuResponse> CreateMenu (CreateMenuRequest request)
        {
            return await menuCommand.CreatMenu(request);
        }
        [Authorize]
        [HttpPut("menu/{menuCode}")]
        [ActionName("")]
        public async Task<MenuResponse> UpdateMenu (UpdateMenuRequest request, string menuCode)
        {
            return await menuCommand.UpdateMenu(request, menuCode);
        }
        [Authorize]
        [HttpDelete("menu/{menuCode}")]
        [ActionName("")]
        public async Task<MenuResponse> DeleteMenu (string menuCode)
        {
            return await menuCommand.DeleteMenu(menuCode);
        }
    }
}