using System;

namespace Generic.Api.Dtos
{
    public class UserForReturnDto
    {        
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }        
        public DateTime DateOfBirth { get; set; }        
        public DateTime Created { get; set; }
        public string[] RoleNames { get; set; }
    }
}