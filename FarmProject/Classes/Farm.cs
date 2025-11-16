using System;
using System.Collections.Generic;
using System.Text;

namespace FarmProject.Classes
{
    public class Farm
    {
        private List<Plot> plots;
        private List<Worker> workers;
        private Manager manager;

        public string Name { get; set; }
        public string Location { get; set; }
        public double Balance { get; set; }

        public List<Plot> Plots
        {
            get { return plots; }
        }
        public List<Worker> Workers
        {
            get { return workers; }
        }
        public Manager Manager
        {
            get { return manager; }
            set { manager = value; }
        }

        public Farm(string name, string location, double startBalance)
        {
            throw new NotImplementedException();
        }

        public bool BuyPlot(Plot plot)
        {
            throw new NotImplementedException();
        }

        public void HireWorker(Worker worker)
        {
            throw new NotImplementedException();
        }

        public void FireWorker(Worker worker)
        {
            throw new NotImplementedException();
        }

        public void AssignWorkerToPlot(Worker worker, Plot plot)
        {
            throw new NotImplementedException();
        }

        public bool UpgradePlot(Plot plot)
        {
            throw new NotImplementedException();
        }

        public void CollectAllIncome()
        {
            throw new NotImplementedException();
        }
    }
}
