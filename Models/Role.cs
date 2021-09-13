using System.Collections.Generic;

namespace Timetracker
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }


        public Role()
        {
            Employees = new List<Employee>();
        }
    }
}