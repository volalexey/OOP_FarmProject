using FarmProject.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmProject.Classes
{
    public class AnimalPen : Plot
    {
        public AnimalType CurrentAnimal { get; set; }
        public int AnimalCount { get; set; }
        public int MaxCapacity { get; set; }

        public AnimalPen(int size, double cost, double income)
        {
            throw new NotImplementedException();
        }

        public override double CalculateIncome()
        {
            throw new NotImplementedException();
        }

        public override void Upgrade()
        {
            throw new NotImplementedException();
        }

        public bool AddAnimals(int count)
        {
            throw new NotImplementedException();
        }
    }
}
