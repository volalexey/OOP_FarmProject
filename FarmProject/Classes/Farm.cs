using System;
using System.Collections.Generic;

namespace FarmProject.Classes
{
    public class Farm
    {
        private List<Plot> plots;
        private List<Worker> workers;
        private Manager manager;
        private double balance;

        public event Action<double> OnBalanceChanged;

        public event EventHandler OnNewDay;

        public string Name { get; set; }
        public string Location { get; set; }
        public double Balance
        {
            get { return balance; }
            set
            {
                if (balance != value)
                {
                    balance = value;
                    OnBalanceChanged?.Invoke(balance);
                }
            }
        }

        public List<Plot> Plots => plots;
        public List<Worker> Workers => workers;
        public Manager Manager
        {
            get { return manager; }
            set { manager = value; }
        }

        public Farm(string name, string location, double startBalance)
        {
            Name = name;
            Location = location;
            balance = startBalance;
            plots = new List<Plot>();
            workers = new List<Worker>();
        }

        public bool BuyPlot(Plot plot)
        {
            if (Balance >= plot.BaseCost)
            {
                Balance -= plot.BaseCost;
                plots.Add(plot);
                Console.WriteLine("The plot has been successfully purchased!");
                return true;
            }
            Console.WriteLine("Insufficient funds to purchase a plot.");
            return false;
        }

        public void HireWorker(Worker worker)
        {
            workers.Add(worker);
            Console.WriteLine($"Worker {worker.Name} hired.");
        }

        public void FireWorker(Worker worker)
        {
            if (workers.Contains(worker))
            {
                workers.Remove(worker);

                foreach (var plot in plots)
                {
                    plot.RemoveWorker(worker);
                }
                Console.WriteLine($"Worker {worker.Name} fired.");
            }
        }

        public void AssignWorkerToPlot(Worker worker, Plot plot)
        {
            if (workers.Contains(worker) && plots.Contains(plot))
            {
                plot.AssignWorker(worker);
                Console.WriteLine($"{worker.Name} assigned to plot.");
            }
            else
            {
                Console.WriteLine("Wrong employee or plot.");
            }
        }

        public bool UpgradePlot(Plot plot)
        {
            double upgradeCost = plot.BaseCost * 0.5;

            if (Balance >= upgradeCost)
            {
                Balance -= upgradeCost;
                plot.Upgrade();
                Console.WriteLine($"Upgrade successful! Cost: ${upgradeCost:F2}. Remaining Balance: ${Balance:F2}");
                return true;
            }

            Console.WriteLine($"Not enough funds! Cost: ${upgradeCost:F2}, but you have ${Balance:F2}");
            return false;
        }

        public double CollectAllIncome()
        {
            double grossIncome = 0;
            foreach (var plot in plots) grossIncome += plot.CalculateIncome();

            double managerSalary = (manager != null) ? manager.Salary : 0;
            double workersSalary = 0;
            foreach (var worker in workers) workersSalary += worker.Salary;

            double totalExpenses = managerSalary + workersSalary;
            Balance += (grossIncome - totalExpenses);

            OnNewDay?.Invoke(this, EventArgs.Empty);

            Console.WriteLine("\n=== FINANCIAL REPORT ===");
            Console.WriteLine($"Gross Income (Plots): +${grossIncome:F2}");
            Console.WriteLine($"Expenses (Salaries):  -${totalExpenses:F2} (Manager: {managerSalary}, Workers: {workersSalary})");
            Console.WriteLine($"---------------------------");
            Console.WriteLine($"Net Profit:           ${(grossIncome - totalExpenses):F2}");
            Console.WriteLine($"New Balance:          ${Balance:F2}");

            return Balance;
        }

        public List<Worker> FindWorkers(Predicate<Worker> match)
        {
            List<Worker> found = new List<Worker>();
            foreach (var w in workers)
            {
                if (match(w))
                {
                    found.Add(w);
                }
            }
            return found;
        }
    }
}