using System;

namespace t1.Models
{
    public class Department
    {
        public long DepartmentId { get; set; }
        public string Name { get; set; }
        public Guid? Id { get; set; }
        
        public virtual User User { get; set; }
    }
}