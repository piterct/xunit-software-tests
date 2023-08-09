using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public  class Employee: Person
    {
        public double  Salary { get; set; }
        public EJobLevel JobLevel { get; set; }
        public IList<string> Skills { get; set; }

        public Employee(string name, double salary)
        {
            Name = string.IsNullOrEmpty(name) ? "Fulano" : name;
            SetSalary(salary);
            SetSkills();
        }


        public void SetSalary(double salary)
        {
            if (salary < 500) throw new Exception("Salary less than allowed");

            Salary = salary;
            if (salary < 2000) JobLevel = EJobLevel.Junior;
            else if (salary >= 2000 && salary < 8000) JobLevel = EJobLevel.Middle;
            else if (salary >= 8000) JobLevel = EJobLevel.Senior;
        }

        private void SetSkills()
        {
            var skills = new List<string>()
            {
                "Programming logic",
                "OOP"
            };

            Skills = skills;

            switch(JobLevel)
            {
                case EJobLevel.Middle:
                    Skills.Add("Tests");
                    break;
                case EJobLevel.Senior:
                    Skills.Add("Tests");
                    Skills.Add("Microservices");
                    break;
            }
        }
    }
}
