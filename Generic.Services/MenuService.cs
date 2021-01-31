using Generic.Core;
using Generic.Core.Models;
using Generic.Core.Repositories;
using Generic.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*
The MenuService class
Contains all Bussines Logic can be Injected many repositories that you considered necesary 
for make validations, rules, etc. Can declare specifies methods and functions 
declaring names and parameters that can be same of the IMenuServiceRepository declarations
*/
/// <summary>
/// The MenuService class
/// Contains all Bussines Logic can be Injected many repositories that you considered necesary 
/// for make validations, rules, etc. Can declare specifies methods and functions 
/// declaring names and parameters that can be same of the IMenuServiceRepository declarations
/// </summary> 
/// 

///Agrego este comentario para probar CI
namespace Generic.Services
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// MenuService receive a IUnitOfWork Interface that encapsulates all Names of the All repositories
        /// that the Data Layer has for Data Base Operations. 
        /// </summary>
        /// <param name="unitOfWork">The Interface IUnitOfWork </param>   
        public MenuService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Get the first or Default Menu Register filter by Id 
        /// </summary>
        /// <param name="id">Id of Menu for the search</param>
        /// <returns>BaseResponse<Menu> with the result of the search By Id</returns>
        public async Task<BaseResponse<Menu>> GetMenuById(int id)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<Menu> result = new BaseResponse<Menu>();
            List<string> err = new List<string>();

            try
            {
                //Using _unitOfWork call to a MenuRepository and through of the 
                //GetByIdAsync GENERIC method Search the first or Default
                

                //ESTA ES LA OPCION DE QUE NO CONVIERTE dynamic A MENU
                //result.DataResponse = await  _unitOfWork.MenuRepository.GetMenuById(id);

                //ESTA ES LA OPCION OBTIENE DEL GENERICO 
                result.DataResponse = await _unitOfWork.MenuRepository.GetByIdAsync(id);
                result.DataResponse.RoleMenus.Clear();

                //If the Query was Successful then in the result this flat in true
                result.Successful = true; 
            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en MenuService -> GetMenuById " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result; 
        }
        /// <summary>
        /// Get All registers of  Menu Register 
        /// </summary>
        /// <returns>BaseResponse<IEnumerable<Menu>> object with result of the search All </returns>
        public async Task<BaseResponse<IEnumerable<Menu>>> GetMenus()
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<IEnumerable<Menu>> result = new BaseResponse<IEnumerable<Menu>>();
            List<string> err = new List<string>();

            try
            {
                //Using _unitOfWork call to a MenuRepository and through of the 
                //GetAllAsync GENERIC method Search All register
                
                //ESTA ES LA OPCION DE QUE NO CONVIERTE ANONIMUS A MENU
                //result.DataResponse = await  _unitOfWork.MenuRepository.GetMenus();

                result.DataResponse = await  _unitOfWork.MenuRepository.GetAllAsync();

                //If the Query was Successful then in the result this flat in true  
                result.Successful = true; 
            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en MenuService -> GetMenus " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result;
        }
        /// <summary>
        /// Function thah Create a register for Menu  
        /// </summary>
        /// <param name="MenuToBeCreatedModel">Menu for be inserted in the table<</param>        
        /// <returns>Task<BaseResponse<Menu>>  result object with the response from Repository Funtion AddAsync</returns>
        public async Task<BaseResponse<Menu>> CreateMenu(Menu MenuToBeCreatedModel)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<Menu> result = new BaseResponse<Menu>();
            List<string> err = new List<string>();
            List<DetailResponse> detailResponse = new  List<DetailResponse>();

            try
            {
                await _unitOfWork.MenuRepository.AddAsync(MenuToBeCreatedModel);
                await _unitOfWork.CommitAsync();

                //Set Successful in true because the commit was completed     
                result.Successful = true;

                //Set in Data Response of result object   
                result.DataResponse = await _unitOfWork.MenuRepository.GetByIdAsync( MenuToBeCreatedModel.Id);

                //Set in Details local variable  object a message for successful execution in the Create
                detailResponse.Add(result.AddDetailResponse (MenuToBeCreatedModel.Id, "Regitro exitoso"));

                //Set Details from local variable to result before return
                result.Details = detailResponse;

            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en MenuService -> CreateMenu " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result;
        }
        /// <summary>
        /// Function thah Update a register for Menu  
        /// </summary>
        /// <param name="MenuToBeUpdateModel">Instance of Menu for be Updated</param>
        /// <param name="MenuForUpdateModel">Instance of Menu with the Data to Update</param>
        /// <returns></returns>
        public async Task<BaseResponse<Menu>>  UpdateMenu(Menu MenuToBeUpdateModel , Menu MenuForUpdateModel)
        {

            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<Menu> result = new BaseResponse<Menu>();
            List<string> err = new List<string>(); 
            List<DetailResponse> detailResponse = new  List<DetailResponse>();

            try
            {
                //Set in the object TO BE UPDATED (ORIGIN) the changues from object FOR UPDATE                
                MenuToBeUpdateModel.ParentId = MenuForUpdateModel.ParentId;
                MenuToBeUpdateModel.Path = MenuForUpdateModel.Path;
                MenuToBeUpdateModel.Status = MenuForUpdateModel.Status;
                MenuToBeUpdateModel.Title = MenuForUpdateModel.Title;             
            

                //Call commit of the changues in the past step            
                await _unitOfWork.CommitAsync();  

                //Set Successful in true because the commit was completed     
                result.Successful = true;

                //Set in Data Response of result object   
                result.DataResponse = await _unitOfWork.MenuRepository.GetByIdAsync(MenuToBeUpdateModel.Id);

                //Set in Details local variable  object a message for successful execution in the Update
                detailResponse.Add(result.AddDetailResponse (MenuToBeUpdateModel.Id, "Actualización realizada correctamente"));

                //Set Details from local variable to result before return
                result.Details = detailResponse;
                
            }
            catch (System.Exception ex )
            {
                //If exist a Error un the transaction then set Error Detail for return in the result
                err.Add("Error en MenuService -> UpdateMenu " + ex.Message);
                result.errors = err;                
            }

            //return result FROM SERVICE object
            return result;  
            
        }
        /// <summary>
        /// Get the first or Default Menu Register filter by Name 
        /// </summary>
        /// <param name="id">Id of Menu for the search</param>
        /// <returns>BaseResponse<Menu> with the result of the search By Name</returns>
        // public async Task<BaseResponse<Menu>>  GetMenuByRoleId(int roleId)
        // {
        //     //Declare variables for result and errors for be filled with the response of _unitOfWork
        //     BaseResponse<Menu> result = new BaseResponse<Menu>();
        //     List<string> err = new List<string>();

        //     try
        //     {
        //         //Using _unitOfWork call to a MenuRepository and through of the 
        //         //GetByIdAsync GENERIC method Search the first or Default
        //         result.DataResponse = await _unitOfWork.MenuRepository.GetMenuByName(name);

        //         //If the Query was Successful then in the result this flat in true
        //         result.Successful = true; 
        //     }
        //     catch (System.Exception ex )
        //     {
        //         //If exist a Exception it's catched and Set in err List the Message 
        //         err.Add("Error en MenuService -> GetMenu " + ex.Message);
        //         //Set in the result errors object the exception message
        //         result.errors = err;
        //     }          

        //     return result;
            
        // }
    }
}
