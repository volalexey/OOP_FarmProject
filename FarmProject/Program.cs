using FarmProject.Classes;
using FarmProject.Types;

namespace FarmProject
{
    class Program
    {
        static Farm myFarm;
        static Random rnd = new Random();

        static string[] names = { "Ivan", "Petro", "Oksana", "Maria", "John", "Alex", "Bob", "Alice", "Taras", "Sofia" };

        static void Main(string[] args)
        {
            InitializeFarm();

            myFarm.OnBalanceChanged += (newBalance) =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($" >>> BANK NOTIFICATION: Balance updated. New: ${newBalance:F2}");
                Console.ResetColor();
            };

            myFarm.OnNewDay += MyFarm_OnNewDay;

            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                PrintHeader();
                PrintMenuOptions();

                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ShowFarmStatus(); break;
                    case "2": ShopPlotsMenu(); break;
                    case "3": ShopWorkersMenu(); break;
                    case "4": AssignWorkerToTask(); break;
                    case "5": ManagePlotAction(); break;
                    case "6": UpgradeExistingPlot(); break;
                    case "7": CollectDailyIncome(); break;
                    case "8": GiveBonusesMenu(); break;
                    case "9": FindWorkersMenu(); break;
                    case "0": isRunning = false; break;
                    default: break;
                }
            }
        }

        private static void MyFarm_OnNewDay(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n*** DAY ENDED! STATISTICS SAVED. ***");
            Console.ResetColor();
        }

        static void InitializeFarm()
        {
            myFarm = new Farm("Alex Farm", "Ukraine", 2000);
            myFarm.Manager = new Manager("Boss", 40, 100);
            myFarm.BuyPlot(new CropPlot(10, 0, 20));
        }

        static void GiveBonusesMenu()
        {
            Console.WriteLine("\n--- BONUS DISTRIBUTION ---");
            Console.WriteLine("Choose bonus strategy:");
            Console.WriteLine("1. Efficiency based (Eff * 10$)");
            Console.WriteLine("2. Hard work based (Salary * 10%)");

            string c = Console.ReadLine();
            BonusCalculation strategy = null;

            if (c == "1")
            {
                strategy = (w) => w.EfficiencyMultiplier * 10;
            }
            else if (c == "2")
            {
                strategy = (w) => w.Salary * 0.1;
            }
            else return;

            double paid = myFarm.Manager.DistributeBonuses(
                myFarm.Workers,
                strategy,
                msg => Console.WriteLine(msg)
            );

            if (paid > 0)
            {
                myFarm.Balance -= paid;
                Console.WriteLine($"Total bonuses paid: ${paid:F2}");
            }
            WaitForKey();
        }

        static void FindWorkersMenu()
        {
            Console.WriteLine("\n--- HR SEARCH (Predicate) ---");
            Console.WriteLine("1. Find High Salary (> $500)");
            Console.WriteLine("2. Find Young Workers (< 30 y.o.)");

            string c = Console.ReadLine();
            Predicate<Worker> filter = null;

            if (c == "1") filter = w => w.Salary > 500;
            else if (c == "2") filter = w => w.Age < 30;
            else return;

            var results = myFarm.FindWorkers(filter);

            Console.WriteLine($"Found {results.Count} workers:");
            foreach (var w in results)
            {
                Console.WriteLine($"- {w.Name} (Age: {w.Age}, Sal: {w.Salary})");
            }
            Console.WriteLine("Press Enter...");
            Console.ReadLine();
        }

        static void PrintHeader()
        {
            Console.WriteLine("==============================================");
            Console.WriteLine($" FARM: {myFarm.Name} | BALANCE: ${myFarm.Balance:F2}");
            Console.WriteLine("==============================================");
        }

        static void PrintMenuOptions()
        {
            Console.WriteLine("1. Show Farm Status");
            Console.WriteLine("2. BUY PLOT");
            Console.WriteLine("3. HIRE WORKER");
            Console.WriteLine("4. Assign Worker to Plot");
            Console.WriteLine("5. Manage Plot");
            Console.WriteLine("6. Upgrade Plot");
            Console.WriteLine("7. [END DAY] Pay Salaries & Collect Income");
            Console.WriteLine("8. [DELEGATE] Distribute Bonuses");
            Console.WriteLine("9. [PREDICATE] Find Workers");
            Console.WriteLine("0. Exit");
            Console.WriteLine("----------------------------------------------");
        }

        static void WaitForKey()
        {
            Console.WriteLine("\nPress Enter...");
            Console.ReadLine();
        }

        static List<Plot> GenerateRandomPlots()
        {
            List<Plot> options = new List<Plot>();
            for (int i = 0; i < 3; i++)
            {
                int type = rnd.Next(0, 2);
                int size = rnd.Next(5, 25);
                double cost = size * rnd.Next(40, 60);
                double income = size * rnd.Next(2, 5);

                if (type == 0)
                    options.Add(new CropPlot(size, cost, income));
                else
                    options.Add(new AnimalPen(size, cost, income));
            }
            return options;
        }

        static List<Worker> GenerateRandomWorkers()
        {
            List<Worker> options = new List<Worker>();
            for (int i = 0; i < 3; i++)
            {
                string name = names[rnd.Next(names.Length)];
                int age = rnd.Next(18, 60);
                double salary = rnd.Next(200, 800);

                double efficiency = 0.5 + (salary / 1000.0) + (rnd.NextDouble() * 0.5);

                var w = new Worker(name, age, salary);
                w.EfficiencyMultiplier = Math.Round(efficiency, 2);
                options.Add(w);
            }
            return options;
        }

        static void ShopPlotsMenu()
        {
            var offers = GenerateRandomPlots();

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== REAL ESTATE AGENCY (Balance: ${myFarm.Balance:F2}) ===");
                Console.WriteLine("Available offers:");

                for (int i = 0; i < offers.Count; i++)
                {
                    var p = offers[i];
                    string type = p is CropPlot ? "CROP FIELD" : "ANIMAL PEN";
                    Console.WriteLine($"{i + 1}. [{type}] Size: {p.Size} | Cost: ${p.BaseCost:F0} | Base Income: {p.BaseIncome:F1}");
                }

                Console.WriteLine("-----------------------------");
                Console.WriteLine("4. REROLL offers (Cost: $50)");
                Console.WriteLine("0. Back to Menu");
                Console.Write("> ");

                string choice = Console.ReadLine();

                if (choice == "0") return;

                if (choice == "4")
                {
                    if (myFarm.Balance >= 50)
                    {
                        myFarm.Balance -= 50;
                        offers = GenerateRandomPlots();
                        Console.WriteLine("Offers refreshed!");
                    }
                    else
                    {
                        Console.WriteLine("Not enough money to reroll!");
                        Console.ReadLine();
                    }
                    continue;
                }

                if (int.TryParse(choice, out int idx) && idx >= 1 && idx <= 3)
                {
                    var selectedPlot = offers[idx - 1];

                    if (myFarm.BuyPlot(selectedPlot))
                    {
                        WaitForKey();
                        return;
                    }
                    else
                    {
                        WaitForKey();
                    }
                }
            }
        }

        static void ShopWorkersMenu()
        {
            var candidates = GenerateRandomWorkers();

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== HR AGENCY (Balance: ${myFarm.Balance:F2}) ===");
                Console.WriteLine("Candidates for hire:");

                for (int i = 0; i < candidates.Count; i++)
                {
                    var w = candidates[i];
                    Console.WriteLine($"{i + 1}. {w.Name} (Age: {w.Age}) | Salary: ${w.Salary}/day | Efficiency: x{w.EfficiencyMultiplier}");
                }

                Console.WriteLine("-----------------------------");
                Console.WriteLine("4. REROLL candidates (Cost: $20)");
                Console.WriteLine("0. Back to Menu");
                Console.Write("> ");

                string choice = Console.ReadLine();

                if (choice == "0") return;

                if (choice == "4")
                {
                    if (myFarm.Balance >= 20)
                    {
                        myFarm.Balance -= 20;
                        candidates = GenerateRandomWorkers();
                    }
                    else
                    {
                        Console.WriteLine("Not enough money to reroll!");
                        Console.ReadLine();
                    }
                    continue;
                }

                if (int.TryParse(choice, out int idx) && idx >= 1 && idx <= 3)
                {
                    var selectedWorker = candidates[idx - 1];
                    myFarm.HireWorker(selectedWorker);
                    WaitForKey();
                    return;
                }
            }
        }

        static void ShowFarmStatus()
        {
            Console.WriteLine("\n--- PLOTS ---");
            for (int i = 0; i < myFarm.Plots.Count; i++)
            {
                var p = myFarm.Plots[i];
                string details = "";
                if (p is CropPlot cp) details = $"Crop: {cp.CurrentCrop}";
                if (p is AnimalPen ap) details = $"Animals: {ap.AnimalCount}/{ap.MaxCapacity}";

                Console.WriteLine($"#{i + 1} [{p.GetType().Name}] Inc: {p.CalculateIncome():F1} | {details}");
            }

            Console.WriteLine("\n--- WORKERS ---");
            myFarm.Workers.Sort();
            foreach (var w in myFarm.Workers)
                Console.WriteLine($"- {w.Name}: Sal ${w.Salary}, Eff x{w.EfficiencyMultiplier}");

            WaitForKey();
        }

        static void AssignWorkerToTask()
        {
            if (myFarm.Workers.Count == 0 || myFarm.Plots.Count == 0) { Console.WriteLine("Nothing to assign."); WaitForKey(); return; }

            Console.WriteLine("WORKERS:");
            for (int i = 0; i < myFarm.Workers.Count; i++) Console.WriteLine($"{i + 1}. {myFarm.Workers[i].Name}");
            Console.WriteLine("PLOTS:");
            for (int i = 0; i < myFarm.Plots.Count; i++) Console.WriteLine($"{i + 1}. {myFarm.Plots[i].GetType().Name}");

            Console.Write("Worker ID: "); int wId = int.Parse(Console.ReadLine()) - 1;
            Console.Write("Plot ID: "); int pId = int.Parse(Console.ReadLine()) - 1;

            if (wId >= 0 && wId < myFarm.Workers.Count && pId >= 0 && pId < myFarm.Plots.Count)
                myFarm.AssignWorkerToPlot(myFarm.Workers[wId], myFarm.Plots[pId]);

            WaitForKey();
        }

        static void ManagePlotAction()
        {
            Console.Write("Plot ID: ");
            if (!int.TryParse(Console.ReadLine(), out int i)) return;
            i--;
            if (i >= 0 && i < myFarm.Plots.Count)
            {
                if (myFarm.Plots[i] is CropPlot cp)
                {
                    Console.WriteLine("0-Wheat, 1-Corn, 2-Potato");
                    cp.Plant((CropType)int.Parse(Console.ReadLine()));
                }
                else if (myFarm.Plots[i] is AnimalPen ap)
                {
                    Console.WriteLine("Count to buy:");
                    ap.AddAnimals(int.Parse(Console.ReadLine()));
                }
            }
            WaitForKey();
        }

        static void UpgradeExistingPlot()
        {
            Console.Write("Plot ID: ");
            if (!int.TryParse(Console.ReadLine(), out int i)) return;
            i--;
            if (i >= 0 && i < myFarm.Plots.Count) myFarm.UpgradePlot(myFarm.Plots[i]);
            WaitForKey();
        }

        static void CollectDailyIncome()
        {
            myFarm.CollectAllIncome();
            WaitForKey();
        }
    }
}