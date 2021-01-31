using System;
using System.Collections.Generic;

namespace Generic.Core.Models
{
    public class Menu
    {
        public int Id { get; set; }
        
        public string Path { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }

        public int ParentId { get; set; }    

        public int Status { get; set; }    

        public virtual ICollection<RoleMenu> RoleMenus { get; set; }       
        
    }
}