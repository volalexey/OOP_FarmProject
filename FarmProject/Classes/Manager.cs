using FarmProject.Interfaces;
using System;

namespace FarmProject.Classes
{
    public delegate double BonusCalculation(Worker worker);
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

        public double DistributeBonuses(List<Worker> workers, BonusCalculation calcStrategy, Action<string> logAction)
        {
            double totalBonuses = 0;

            foreach (var w in workers)
            {
                double bonus = calcStrategy(w);

                if (bonus > 0)
                {
                    totalBonuses += bonus;
                    logAction?.Invoke($"[BONUS] {w.Name}: ${bonus:F2}");
                }
            }
            return totalBonuses;
        }
    }
}