using FarmProject.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmProjectTest
{
    [TestClass]
    public class FarmTests
    {
        private Farm CreateFarmWithBalance(double balance)
        {
            return new Farm("Test Farm", "Location", balance);
        }

        [TestMethod]
        public void Constructor_ShouldSetPropertiesAndInitializeLists()
        {
            // Arrange
            double startBalance = 10000;

            // Act
            Farm farm = new Farm("My Farm", "Location", startBalance);

            // Assert
            Assert.AreEqual("My Farm", farm.Name);
            Assert.AreEqual(startBalance, farm.Balance);
            Assert.IsNotNull(farm.Plots);
            Assert.IsNotNull(farm.Workers);
            Assert.AreEqual(0, farm.Plots.Count);
            Assert.AreEqual(0, farm.Workers.Count);
        }

        [TestMethod]
        public void BuyPlot_ShouldAddPlotAndReduceBalance_WhenAffordable()
        {
            // Arrange
            Farm farm = CreateFarmWithBalance(1000);
            Plot plot = new AnimalPen(5, 800, 10);

            // Act
            bool result = farm.BuyPlot(plot);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(200, farm.Balance);
            Assert.AreEqual(1, farm.Plots.Count);
            Assert.IsTrue(farm.Plots.Contains(plot));
        }

        [TestMethod]
        public void BuyPlot_ShouldNotAddPlot_WhenNotAffordable()
        {
            // Arrange
            Farm farm = CreateFarmWithBalance(500);
            Plot plot = new AnimalPen(5, 800, 10);

            // Act
            bool result = farm.BuyPlot(plot);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(500, farm.Balance);
            Assert.AreEqual(0, farm.Plots.Count);
        }

        [TestMethod]
        public void HireWorker_ShouldAddWorkerToList()
        {
            // Arrange
            Farm farm = CreateFarmWithBalance(1000);
            Worker worker = new Worker("Bob", 40, 300);

            // Act
            farm.HireWorker(worker);

            // Assert
            Assert.AreEqual(1, farm.Workers.Count);
            Assert.IsTrue(farm.Workers.Contains(worker));
        }

        [TestMethod]
        public void FireWorker_ShouldRemoveWorkerFromFarmAndPlot()
        {
            // Arrange
            Farm farm = CreateFarmWithBalance(1000);
            Worker worker = new Worker("Bob", 40, 300) { EfficiencyMultiplier = 2.0 };
            Plot plot = new CropPlot(10, 100, 10);

            farm.HireWorker(worker);
            farm.BuyPlot(plot);
            farm.AssignWorkerToPlot(worker, plot);

            Assert.AreEqual(200, plot.CalculateIncome());

            // Act
            farm.FireWorker(worker);

            // Assert
            Assert.AreEqual(0, farm.Workers.Count);
            Assert.AreEqual(100, plot.CalculateIncome());
        }

        [TestMethod]
        public void CollectAllIncome_ShouldIncreaseBalanceByTotalPlotIncomes()
        {
            // Arrange
            Farm farm = CreateFarmWithBalance(1000);

            AnimalPen pen = new AnimalPen(10, 0, 10);
            pen.AddAnimals(20);
            farm.BuyPlot(pen);

            CropPlot crop = new CropPlot(5, 0, 30);
            farm.BuyPlot(crop);

            double expectedBalance = 1000 + 350;

            // Act
            double balance = farm.CollectAllIncome();

            // Assert
            Assert.AreEqual(expectedBalance, balance);
        }
    }
}
