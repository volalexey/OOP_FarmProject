using FarmProject.Interfaces;

namespace FarmProject.Classes
{
    public abstract class Plot : IProducible
    {
        protected List<Worker> AssignedWorkers = new List<Worker>();

        public int Size { get; set; }
        public double BaseCost { get; set; }
        public double BaseIncome { get; set; }

        protected Plot(int size, double cost, double income)
        {
            Size = size;
            BaseCost = cost;
            BaseIncome = income;
        }

        public abstract double CalculateIncome();

        public virtual void Upgrade()
        {
            BaseIncome *= 1.1;
            Console.WriteLine($"Plot is uppgraded! Base income: {BaseIncome:F2}");
        }

        public void AssignWorker(Worker worker)
        {
            if (!AssignedWorkers.Contains(worker))
            {
                AssignedWorkers.Add(worker);
            }
        }

        public void RemoveWorker(Worker worker)
        {
            if (AssignedWorkers.Contains(worker))
            {
                AssignedWorkers.Remove(worker);
            }
        }

        protected double GetTotalWorkerMultiplier()
        {
            if (AssignedWorkers.Count == 0) return 1.0;

            return AssignedWorkers.Sum(w => w.EfficiencyMultiplier);
        }
    }
}