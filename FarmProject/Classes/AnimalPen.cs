using FarmProject.Types;
using System;

namespace FarmProject.Classes
{
    public class AnimalPen : Plot
    {
        public AnimalType CurrentAnimal { get; set; }
        public int AnimalCount { get; set; }
        public int MaxCapacity { get; set; }

        public AnimalPen(int size, double cost, double income)
            : base(size, cost, income)
        {
            MaxCapacity = size * 5;
            AnimalCount = 0;
            CurrentAnimal = AnimalType.COW;
        }

        public override double CalculateIncome()
        {
            if (AnimalCount == 0) return 0;

            return (BaseIncome * AnimalCount) * GetTotalWorkerMultiplier();
        }

        public override void Upgrade()
        {
            MaxCapacity += 10;
            Console.WriteLine($"The pen has been expanded! Now accommodates {MaxCapacity} animals.");
        }

        public bool AddAnimals(int count)
        {
            if (AnimalCount + count <= MaxCapacity)
            {
                AnimalCount += count;
                return true;
            }
            return false;
        }
    }
}