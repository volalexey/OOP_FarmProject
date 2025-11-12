using FarmProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmProject.Classes
{
    public class Worker : IPerson
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public double Salary { get; set; }
        public double EfficiencyMultiplier { get; set; }

        public Worker(string name, int age, double salary)
        {
            throw new NotImplementedException();
        }

        public void Work()
        {
            throw new NotImplementedException();
        }
    }
}
