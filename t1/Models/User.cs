using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace t1.Models
{
    public class User : IdentityUser<Guid>
    {
        public virtual ICollection<Department> Departments { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User()
        {
            Departments = new HashSet<Department>();
        }
    }
}