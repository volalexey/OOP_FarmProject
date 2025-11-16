using FarmProject.Classes;
using FarmProject.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmProjectTest
{
    [TestClass]
    public class CropPlotTests
    {
        [TestMethod]
        public void Plant_ShouldSetCurrentCrop()
        {
            // Arrange
            CropPlot plot = new CropPlot(10, 500, 20);

            // Act
            plot.Plant(CropType.WHEAT);

            // Assert
            Assert.AreEqual(CropType.WHEAT, plot.CurrentCrop);
        }

        [TestMethod]
        public void CalculateIncome_ShouldUseBaseIncomeAndSize_WhenNoWorkers()
        {
            // Arrange
            int size = 10;
            double baseIncome = 20;
            CropPlot plot = new CropPlot(size, 500, baseIncome);
            double expectedIncome = 20 * 10;

            // Act
            double income = plot.CalculateIncome();

            // Assert
            Assert.AreEqual(expectedIncome, income);
        }

        [TestMethod]
        public void CalculateIncome_ShouldBeBoostedByWorkers()
        {
            // Arrange
            CropPlot plot = new CropPlot(10, 500, 20);

            Worker worker1 = new Worker("Worker 1", 30, 100) { EfficiencyMultiplier = 1.5 };
            plot.AssignWorker(worker1);

            double expectedIncome = (20 * 10) * 1.5;

            // Act
            double income = plot.CalculateIncome();

            // Assert
            Assert.AreEqual(expectedIncome, income);
        }

        [TestMethod]
        public void AssignWorker_ShouldIncreaseIncome()
        {
            // Arrange
            CropPlot plot = new CropPlot(10, 500, 20);
            double incomeBefore = plot.CalculateIncome();

            Worker worker = new Worker("Tester", 30, 100) { EfficiencyMultiplier = 2.0 };

            // Act
            plot.AssignWorker(worker);
            double incomeAfter = plot.CalculateIncome();

            // Assert
            Assert.IsTrue(incomeAfter > incomeBefore);
            Assert.AreEqual(incomeBefore * 2.0, incomeAfter);
        }

        [TestMethod]
        public void RemoveWorker_ShouldDecreaseIncome()
        {
            // Arrange
            CropPlot plot = new CropPlot(10, 500, 20);
            Worker worker = new Worker("Tester", 30, 100) { EfficiencyMultiplier = 2.0 };
            plot.AssignWorker(worker);

            double incomeWithWorker = plot.CalculateIncome();

            // Act
            plot.RemoveWorker(worker);
            double incomeWithoutWorker = plot.CalculateIncome();

            // Assert
            Assert.IsTrue(incomeWithoutWorker < incomeWithWorker);
            Assert.AreEqual(200, incomeWithoutWorker);
        }
    }
}
