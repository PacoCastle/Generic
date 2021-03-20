using System;
 using System.Collections.Generic;
 using System.Security.Claims;
 using System.Threading.Tasks;
 using AutoMapper;
using Generic.Api.Dtos;
 using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Mvc;
 using Generic.Core.Models;
using Generic.Core.Services;
using Generic.Core.Repositories;
/*
The UsersController class
Contains all EndPoints for Get, Post and Put Users Entity
*/
/// <summary>
/// The UsersRepository class
/// Contains all EndPoints for Get, Post and Put Users Entity
/// </summary>    
/// 
namespace Generic.Api.Controllers
{
    //[AllowAnonymous]
    [Authorize]
    [Route("api/[controller]")]
     [ApiController]
     public class UsersController : ControllerBase
     {
         private readonly IMapper _mapper;

         private readonly IUserService _service;

         private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// UsersController Constructor Initialize the Injected Interfaces for use it.
        /// </summary>
        /// <param name="mapper">Interface that contain mappings between DTO's and Models</param>
        /// <param name="service">Interface that contain acces to Service funtions and methods of Userservice</param>
        public UsersController(IUserService service, IMapper mapper, IUnitOfWork unitOfWork)
         {
             _mapper = mapper;
             _service = service;
            _unitOfWork = unitOfWork;
         }
        /// <summary>
        /// Search in User filter By Id
        /// </summary>
        /// <param name="id">Id for Search Register in Data Base for </param>
        /// <returns>Object wit Status of execution and Data Searched and a Status of request</returns>
        
         [HttpGet("{id}", Name = "GetUser")]
         public async Task<IActionResult> GetUser(int id)
         {
             //BaseResponse<UserForListDto> resultMapped = new BaseResponse<UserForListDto>();
              //Get ther response from call GetUserById from Service that retorn object with data for be validate
              var serviceResult  = await _service.GetUserById(id);

              //If the Service response Successful the Query was executed
              if (serviceResult.Successful)
              {
                  //If the search doesn't has Data filtering by Id then return NotFound (404)
                  if (serviceResult.DataResponse == null)
                    return NotFound(serviceResult);

                  //If the search has Data filtering by Id then return Ok (200)  and return the register Serached
                  return Ok(serviceResult);

              }
              //If the Service response isn't Successful then ocurred some wrong and return (400)
              return BadRequest(serviceResult); 
         }
        /// <summary>
        /// Search All User 
        /// </summary>
        /// <returns>Object wit Status of execution and Data Searched and a Status of request</returns>
         [HttpGet(Name = "GetUsers")]
         public async Task<IActionResult> GetUsers()
         {
              //Get ther response from call GetUsers from Service that retorn object with data for be validate
              var serviceResult = await _service.GetUsers();

              //If the Service response Successful the Query was executed  
              if (serviceResult.Successful)
              {
                  //If the search doesn't has Data filtering by Id then return NotFound (404)
                  if (serviceResult.DataResponse == null)
                    return NotFound(serviceResult);
                  
                  //If the search has Data filtering by Id then return Ok (200) and return the register Serached
                  return Ok(serviceResult);
              }
              //If the Service response isn't Successful then ocurred some wrong and return (400)
              return BadRequest(serviceResult);     
         }
        /// <summary>
        /// Create User register
        /// </summary>
        /// <param name="UserForCreationDto">DTO That contains the properties for Insert in Data Base</param>
        /// <returns>Object wit Status of execution and Data Created and a Status of request</returns>
          [HttpPost]
         public async Task<IActionResult> CreateUser(UserForCreateDto userForCreateDto)
         {
            List<string> err = new List<string>();
            //Search if de userName to be Updated get Data for Create
            var UserFromRepo = await _service.GetUserByUserName(userForCreateDto.UserName);

            //If not exist Data for the id parameter return 404 and Empty DataResponse object 
            if (UserFromRepo.DataResponse != null) {
                err.Add("El usuario  " + userForCreateDto.UserName + " ya existe");
                //Set in the result errors object the exception message
                UserFromRepo.errors = err;

                return Conflict(UserFromRepo);
            }

            // Map to a User for Send to Service
            var UserForCreate = _mapper.Map<User>(userForCreateDto); 

             //Get ther response from call GetUsers from Service that retorn object with data for be validate
             var serviceResult = await _service.CreateUser(UserForCreate, userForCreateDto.Password);

            //If the Service response Successful the Query was executed  and return the register Created
             if (serviceResult.Successful)
             {
                 return Ok(serviceResult);                 
             }
            //If the Service response isn't Successful then ocurred some wrong and return (400)
             return BadRequest(serviceResult);   
         }
        /// <summary>
        /// Update User register
        /// </summary>
        /// <param name="id">Id of Register to be Updated</param>
        /// <param name="UserForUpdateDto">DTO that contains properties to be Updated </param>
        /// <returns>Object wit Status of execution and Data Updated and a Status of request</returns>
      [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto UserForUpdateDto)
        {
            //Search if de Id to be Updated get Data for Update
            var UserToBeUpdated = await _unitOfWork.UserRepository.GetByIdAsync(id);

            //If not exist Data for the id parameter return 404 and Empty DataResponse object 
            if (UserToBeUpdated == null)
                return NotFound();

            //If exist Data it's Mapped to a User for Send to Service
            var UserForUpdate = _mapper.Map<User>(UserForUpdateDto); 
            
            //Get Response from Service and UpdateUser sendig Object to be Updated and Data for make the Update 
            var serviceResult = await _service.UpdateUser(UserToBeUpdated, UserForUpdate, UserForUpdateDto.Password);

            //if the Update was Successful then return 200 and an Object with DataResponse Updated
            if(serviceResult.Successful)
            {
                return Ok(serviceResult);
            }        

            //if the Update wasn't Successful then return 400 and an Object with Error information
            return BadRequest(serviceResult);   
        }
    }
 } 