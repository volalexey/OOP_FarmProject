using FarmProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmProject.Classes
{
    public class Manager : IPerson
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public double Salary { get; set; }

        public Manager(string name, int age, double salary)
        {
            throw new NotImplementedException();
        }

        public void ManageFarm()
        {
            throw new NotImplementedException();
        }
    }
}
