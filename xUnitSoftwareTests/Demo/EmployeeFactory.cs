using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class EmployeeFactory
    {
        public static Employee Create(string name, double salary)
        {
            return new Employee(name, salary);
        }
    }
}
