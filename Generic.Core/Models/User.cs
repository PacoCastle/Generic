using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Generic.Core.Models
{
    public class User : IdentityUser<int>
    {

        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public int Status { get; set; } = 1;

        public virtual ICollection<UserRole> UserRoles { get; set; }
         [NotMapped]        
         public virtual ICollection<String> RoleNames { get; set; }

        [NotMapped]
        public String token { get; set; }

        [NotMapped]
        public virtual ICollection<Role> Roles { get; set; }
        [NotMapped]
        public virtual ICollection<Role> UnAssignedRoles { get; set; }

        //public virtual ICollection<RoleMenu> RoleMenu { get; set; }
    }
}