namespace Generic.Api.Dtos
{
    public class UserForUpdateDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; } 

        public string Password { get; set; }
        public string Email { get; set; }      
        public string[] RoleNames { get; set; }

        public int Status { get; set;  }

        public string Sexo { get; set; }
    }
}