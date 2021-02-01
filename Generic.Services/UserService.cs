using Generic.Core;
using Generic.Core.Models;
using Generic.Core.Repositories;
using Generic.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*
The UserService class
Contains all Bussines Logic can be Injected many repositories that you considered necesary 
for make validations, rules, etc. Can declare specifies methods and functions 
declaring names and parameters that can be same of the IUserServiceRepository declarations
*/
/// <summary>
/// The UserService class
/// Contains all Bussines Logic can be Injected many repositories that you considered necesary 
/// for make validations, rules, etc. Can declare specifies methods and functions 
/// declaring names and parameters that can be same of the IUserServiceRepository declarations
/// </summary> 
/// 


namespace Generic.Services

    //public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// UserService receive a IUnitOfWork Interface that encapsulates all Names of the All repositories
        /// that the Data Layer has for Data Base Operations. 
        /// </summary>
        /// <param name="unitOfWork">The Interface IUnitOfWork </param>   
        public UserService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Get the first or Default User Register filter by Id 
        /// </summary>
        /// <param name="id">Id of User for the search</param>
        /// <returns>BaseResponse<User> with the result of the search By Id</returns>
        public async Task<BaseResponse<User>> GetUserById(int id)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<User> result = new BaseResponse<User>();
            List<string> err = new List<string>();

            try
            {
                //Using _unitOfWork call to a UserRepository and through of the 
                //GetByIdAsync GENERIC method Search the first or Default
                result.DataResponse = await _unitOfWork.UserRepository.GetUserById(id);

                //If the Query was Successful then in the result this flat in true
                result.Successful = true; 
            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en UserService -> GetUserById " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result; 
        }
        /// <summary>
        /// Get All registers of  User Register 
        /// </summary>
        /// <returns>BaseResponse<IEnumerable<User>> object with result of the search All </returns>
        public async Task<BaseResponse<IEnumerable<User>>> GetUsers()
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<IEnumerable<User>> result = new BaseResponse<IEnumerable<User>>();
            List<string> err = new List<string>();

            try
            {
                //Using _unitOfWork call to a UserRepository and through of the 
                //GetAllAsync GENERIC method Search All register
                result.DataResponse = await  _unitOfWork.UserRepository.GetUsers(); 

                //If the Query was Successful then in the result this flat in true  
                result.Successful = true; 
            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en UserService -> GetUsers " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result;
        }
        /// <summary>
        /// Function thah Create a register for User  
        /// </summary>
        /// <param name="UserToBeCreatedModel">User for be inserted in the table<</param>        
        /// <returns>Task<BaseResponse<User>>  result object with the response from Repository Funtion AddAsync</returns>
        public async Task<BaseResponse<User>> CreateUser(User UserToBeCreatedModel,String password)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<User> result = new BaseResponse<User>();
            List<string> err = new List<string>();
            List<DetailResponse> detailResponse = new  List<DetailResponse>();

            try
            {
                //Send parameters to a validate if exist
                //await validRoles(UserToBeCreatedModel.rol, err);
                string[] RoleNames  = new string[] { };
                RoleNames.Append("Admin");

                UserToBeCreatedModel.RoleNames = RoleNames;

                //Call Method in the repository for create User 
                await _unitOfWork.UserRepository.CreateUser(UserToBeCreatedModel, password);
                
                //Set Successful in true because the commit was completed     
                result.Successful = true;

                //Set in Data Response of result object   
                result.DataResponse = await _unitOfWork.UserRepository.GetUserById(UserToBeCreatedModel.Id);

                //Set in Details local variable  object a message for successful execution in the Create
                detailResponse.Add(result.AddDetailResponse (UserToBeCreatedModel.Id, "Registro exitoso"));

                //Set Details from local variable to result before return
                result.Details = detailResponse;

            }
            catch (System.Exception ex )
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en UserService -> CreateUser " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }          

            return result;
        }

        /// <summary>
        /// Function thah Update a register for User  
        /// </summary>
        /// <param name="UserToBeUpdateModel">Instance of User for be Updated</param>
        /// <param name="UserForUpdateModel">Instance of User with the Data to Update</param>
        /// <returns></returns>
        public async Task<BaseResponse<User>> UpdateUser(User UserToBeUpdateModel , User UserForUpdateModel, string NewPassword)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<User> result = new BaseResponse<User>();
            List<string> err = new List<string>(); 
            List<DetailResponse> detailResponse = new  List<DetailResponse>();

            try
            {
                //Set in the object TO BE UPDATED (ORIGIN) the changues from object FOR UPDATE                
                UserToBeUpdateModel.Name = UserForUpdateModel.Name;
                UserToBeUpdateModel.LastName = UserForUpdateModel.LastName;
                UserToBeUpdateModel.SecondLastName = UserForUpdateModel.SecondLastName;
                UserToBeUpdateModel.Status = UserForUpdateModel.Status;
                UserToBeUpdateModel.Sexo = UserForUpdateModel.Sexo;
                UserToBeUpdateModel.Email = UserForUpdateModel.Email;

                //If the password is not null or Empety then generate Reset
                if (!String.IsNullOrEmpty(NewPassword)) 
                {
                    await _unitOfWork.UserRepository.ResetPassword(UserToBeUpdateModel, NewPassword);
                }

                //Call commit of the changues in the past step            
                await _unitOfWork.CommitAsync(); 
                //await _unitOfWork.UserRepository.UpdateUser(UserToBeUpdateModel);

                //Set Successful in true because the commit was completed     
                result.Successful = true;

                //Set in Data Response of result object   
                var userFromRepo = await this.GetUserById(UserToBeUpdateModel.Id);
                
                //
                result.DataResponse = userFromRepo.DataResponse;

                //Set in Details local variable  object a message for successful execution in the Update
                detailResponse.Add(result.AddDetailResponse (UserToBeUpdateModel.Id, "Actualización realizada correctamente"));

                //Set Details from local variable to result before return
                result.Details = detailResponse;
                
            }
            catch (System.Exception ex )
            {
                //If exist a Error un the transaction then set Error Detail for return in the result
                err.Add("Error en UserService -> UpdateUser " + ex.Message);
                result.errors = err;                
            }

            //return result FROM SERVICE object
            return result;  
            
        }
        /// <summary>
        /// Get the first or Default User Register filter by userName 
        /// </summary>
        /// <param name="userName">userName of User for the search</param>
        /// <returns>BaseResponse<User> with the result of the search By userName</returns>
        public async Task<BaseResponse<User>> GetUserByUserName(String userName)
        {
            //Declare variables for result and errors for be filled with the response of _unitOfWork
            BaseResponse<User> result = new BaseResponse<User>();
            List<string> err = new List<string>();

            try
            {
                //Using _unitOfWork call to a UserRepository and through of the 
                //GetByIdAsync GENERIC method Search the first or Default
                result.DataResponse = await _unitOfWork.UserRepository.GetUserByUserName(userName);

                //If the Query was Successful then in the result this flat in true
                result.Successful = true;
            }
            catch (System.Exception ex)
            {
                //If exist a Exception it's catched and Set in err List the Message 
                err.Add("Error en UserService -> GetUserByUserName " + ex.Message);
                //Set in the result errors object the exception message
                result.errors = err;
            }

            return result;
        }
    }
}
