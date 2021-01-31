using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Generic.Core.Models;

namespace Generic.Core.Services
{
    public interface IMenuService
    {
        Task<BaseResponse<IEnumerable<Menu>>> GetMenus();
        Task<BaseResponse<Menu>> GetMenuById(int id);
        Task<BaseResponse<Menu>>  CreateMenu(Menu Menu);
        Task<BaseResponse<Menu>>  UpdateMenu(Menu Menu , Menu MenuUpdateModel);
        //Task<BaseResponse<Menu>>  GetMenuByName(string name);
    }
}
