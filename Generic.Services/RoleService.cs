using Generic.Core;
using Generic.Core.Models;
using Generic.Core.Repositories;
using Generic.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*
The RoleService class
Contains all Bussines Logic can be Injected many repositories that you considered necesary 
for make validations, rules, etc. Can declare specifies methods and functions 
declaring names and parameters that can be same of the IRoleServiceRepository declarations
*/
/// <summary>
/// The RoleService class
/// Contains all Bussines Logic can be Injected many repositories that you considered necesary 
/// for make validations, rules, etc. Can declare specifies methods and functions 
/// declaring names and parameters that can be same of the IRoleServiceRepository declarations
/// </summary> 
/// 


namespace Generic.Services
{
    public class RoleService : IRoleService
    {

        private readonly IMenuService _service;

        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// RoleService receive a IUnitOfWork Interface that encapsulates all Names of the All repositories
        /// that the Data Layer has for Data Base Operations. 
        /// </summary>
        /// <param name="unitOfWork">The Interface IUnitOfWork </param>   
        public RoleService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Get the first or Default Role Register filter by Name 
        /// </summary>
        /// <param name="id">Id of Role for the search</param>
        /// <returns>BaseResponse<Role> with the result of the search By Id</returns>
        public async Task<BaseResponse<Role>> GetRoleById(int id)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<Role> result = new BaseResponse<Role>();
            List<string> err = new List<string>();

            try
            {
                //Using _unitOfWork call to a RoleRepository and through of the 
                //GetRoleByName GENERIC method Search the first or Default
                result.DataResponse = await _unitOfWork.RoleRepository.GetRoleById(id);

                //If the Query was Successful then in the result this flat in true
                result.Successful = true; 
            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en RoleService -> GetRoleById " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result; 
        } 
        /// <summary>
        /// Get the first or Default Role Register filter by Name 
        /// </summary>
        /// <param name="id">Id of Role for the search</param>
        /// <returns>BaseResponse<Role> with the result of the search By Id</returns>
        public async Task<BaseResponse<string>> GetRoleByName(String name)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<string> result = new BaseResponse<string>();
            List<string> err = new List<string>();

            try
            {
                //Using _unitOfWork call to a RoleRepository and through of the 
                //GetRoleByName GENERIC method Search the first or Default
                result.DataResponse = await _unitOfWork.RoleRepository.GetRoleByName(name);

                //If the Query was Successful then in the result this flat in true
                result.Successful = true; 
            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en RoleService -> GetRoleByName " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result; 
        } 
        /// <summary>
        /// Get All registers of  Role Register 
        /// </summary>
        /// <returns>BaseResponse<IEnumerable<Role>> object with result of the search All </returns>
        public async Task<BaseResponse<IEnumerable<Role>>> GetRoles()
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<IEnumerable<Role>> result = new BaseResponse<IEnumerable<Role>>();
            List<string> err = new List<string>();

            try
            {
                //Using _unitOfWork call to a RoleRepository and through of the 
                //GetAllAsync GENERIC method Search All register
                result.DataResponse = await  _unitOfWork.RoleRepository.GetRoles(); 

                //If the Query was Successful then in the result this flat in true  
                result.Successful = true; 
            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en RoleService -> GetRoles " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result;
        }

      

        /// <summary>
        /// Function thah Create a register for Role  
        /// </summary>
        /// <param name="RoleToBeCreatedModel">Role for be inserted in the table<</param>        
        /// <returns>Task<BaseResponse<Role>>  result object with the response from Repository Funtion AddAsync</returns>
        public async Task<BaseResponse<Role>> CreateRole(Role RoleToBeCreatedModel)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<Role> result = new BaseResponse<Role>();
            List<string> err = new List<string>();
            List<DetailResponse> detailResponse = new  List<DetailResponse>();
            MenuService menuService = new MenuService(_unitOfWork);

            try
            {

                //Extract to a variable the roles to be Updated
                var menuForAdd = RoleToBeCreatedModel.Menus;
                //Send parameters to a validate if exist
                await validMenus(menuForAdd, err);

                //If some or Roles sending doesn't exist, then return 
                if (err.Count > 0)
                {
                    result.errors = err;
                    return result;
                }

                //Set in Data Response of result object   
                result.DataResponse = await _unitOfWork.RoleRepository.CreateRole(RoleToBeCreatedModel);
               
                foreach (var currenMenuId in RoleToBeCreatedModel.Menus)
                {

                    RoleMenu roleMenu = new RoleMenu();
                    roleMenu.RoleId = result.DataResponse.Id;
                    roleMenu.MenuId = currenMenuId.Id;

                    await _unitOfWork.RoleMenuRepository.AddAsync(roleMenu);
                    await _unitOfWork.CommitAsync();
                }

                //Set Successful in true because the commit was completed     
                result.Successful = true;

                //Set in Details local variable  object a message for successful execution in the Create
                detailResponse.Add(result.AddDetailResponse (RoleToBeCreatedModel.Id, "Registro exitoso"));

                //Set Details from local variable to result before return
                result.Details = detailResponse;

            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en RoleService -> CreateRole " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result;
        }
        /// <summary>
        /// Function thah Update a register for Role  
        /// </summary>
        /// <param name="RoleToBeUpdateModel">Instance of Role for be Updated</param>
        /// <param name="RoleForUpdateModel">Instance of Role with the Data to Update</param>
        /// <returns></returns>
        /// 

        public async Task<BaseResponse<Role>> UpdateRole(Role RoleToBeUpdateModel, Role RoleForUpdateModel)
        {
            
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<Role> result = new BaseResponse<Role>();
            List<string> err = new List<string>();
            List<DetailResponse> detailResponse = new List<DetailResponse>();
            List<RoleMenu> lstRoleMenu = new List<RoleMenu>();

            try
            {
                //Extract to a variable the roles to be Updated
                var menuForAdd = RoleForUpdateModel.Menus;
                //Send parameters to a validate if exist
                await validMenus(menuForAdd, err);

                //If some or Roles sending doesn't exist, then return 
                if (err.Count > 0)
                {
                    result.errors = err;
                    return result;
                }

                await _unitOfWork.RoleMenuRepository.RemoveRoleId(RoleToBeUpdateModel.Id);
                foreach (var item in RoleForUpdateModel.Menus)
                {
                    lstRoleMenu.Add(new RoleMenu() { RoleId = RoleToBeUpdateModel.Id, MenuId = item.Id });
                }

                RoleToBeUpdateModel.Status = RoleForUpdateModel.Status;

                await _unitOfWork.RoleMenuRepository.AddRangeAsync(lstRoleMenu);
                await _unitOfWork.CommitAsync();

                result.Successful = true;

                //Set in Details local variable  object a message for successful execution in the Create
                detailResponse.Add(result.AddDetailResponse(RoleToBeUpdateModel.Id, "Registro exitoso"));

                //Set Details from local variable to result before return
                result.Details = detailResponse;
            }
            catch (Exception ex)
            {

                throw;
            }

           
            return result;

        }

        //public async Task<BaseResponse<int>> GetRoleByNameToId(string name)
        //{
        //    //Declare variables for result and errors for be filled with the response of _unitOfWork
        //    BaseResponse<int> result = new BaseResponse<int>();
        //    List<string> err = new List<string>();

        //    try
        //    {
        //        //Using _unitOfWork call to a RoleRepository and through of the 
        //        //GetRoleByName GENERIC method Search the first or Default
        //        result.DataResponse = await _unitOfWork.RoleRepository.GetRoleByNemeToId(name);

        //        //If the Query was Successful then in the result this flat in true
        //        result.Successful = true;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        //If exist a Exception it's catched and Set in err List the Message 
        //        err.Add("Error en RoleService -> GetRoleByNameToId " + ex.Message);
        //        //Set in the result errors object the exception message
        //        result.errors = err;
        //    }

        //    return result;
        //}

        private async Task validMenus(ICollection<Menu> menuForValidate, List<string> err)
        {
            var allMenus = await _unitOfWork.MenuRepository.GetAllAsync();

            var allMenuId = allMenus.Where(r => r.Status == 1)
                                .Select(r => r.Id)
                                .ToList();

           
                var roleDiferences = menuForValidate.Where(x => !allMenuId.Contains(x.Id)).ToList();
                if (roleDiferences.Count > 0)
                {
                    foreach (var currentRole in roleDiferences)
                    {
                        err.Add("El Menu " + currentRole.Id + " no existe el catálogo o no se encuentra activo");
                    }
                }
          
            //If some or Roles sending doesn't exist, then return 
           
        }
    } 
}
