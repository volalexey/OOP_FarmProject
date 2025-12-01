using FarmProject.Interfaces;

namespace FarmProject.Classes
{
    public class Worker : IPerson, IComparable<Worker>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        public double EfficiencyMultiplier { get; set; }

        public Worker(string name, int age, double salary)
        {
            Name = name;
            Age = age;
            Salary = salary;
            EfficiencyMultiplier = 1.0;
        }

        public void Work()
        {
            Console.WriteLine($"{Name} working with {EfficiencyMultiplier} effeciency");
        }

        public int CompareTo(Worker other)
        {
            if (other == null) return 1;
            return other.Salary.CompareTo(this.Salary);
        }
    }
}