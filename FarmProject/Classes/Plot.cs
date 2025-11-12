using FarmProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmProject.Classes
{
    public abstract class Plot : IProducible
    {
        protected List<Worker> AssignedWorkers;

        public int Size { get; set; }
        public double BaseCost { get; set; }
        public double BaseIncome { get; set; }

        public abstract double CalculateIncome();

        public virtual void Upgrade()
        {
            throw new NotImplementedException();
        }

        public void AssignWorker(Worker worker)
        {
            throw new NotImplementedException();
        }

        public void RemoveWorker(Worker worker)
        {
            throw new NotImplementedException();
        }
    }
}
