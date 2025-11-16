using FarmProject.Classes;

namespace FarmProjectTest
{
    [TestClass]
    public class WorkerTests
    {
        [TestMethod]
        [DataRow("Taras", 30, 500.0, 1.1)]
        [DataRow("Maria", 25, 450.0, 1.0)]
        public void Constructor_ShouldSetProperties_WhenCalled(string name, int age, double salary, double multiplier)
        {
            // Act
            Worker worker = new Worker(name, age, salary)
            {
                EfficiencyMultiplier = multiplier
            };

            // Assert
            Assert.AreEqual(name, worker.Name);
            Assert.AreEqual(age, worker.Age);
            Assert.AreEqual(salary, worker.Salary);
            Assert.AreEqual(multiplier, worker.EfficiencyMultiplier);
        }
    }
}
