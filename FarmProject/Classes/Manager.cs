using FarmProject.Interfaces;
using System;

namespace FarmProject.Classes
{
    public class Manager : IPerson
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }

        public Manager(string name, int age, double salary)
        {
            Name = name;
            Age = age;
            Salary = salary;
        }

        public void ManageFarm()
        {
            Console.WriteLine($"{Name} is busy with important matters...");
        }
    }
}