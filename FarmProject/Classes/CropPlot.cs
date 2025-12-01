using FarmProject.Types;

namespace FarmProject.Classes
{
    public class CropPlot : Plot
    {
        public CropType CurrentCrop { get; set; }

        public CropPlot(int size, double cost, double income)
            : base(size, cost, income)
        {
            CurrentCrop = CropType.WHEAT;
        }

        public override double CalculateIncome()
        {
            return (BaseIncome * Size) * GetTotalWorkerMultiplier();
        }

        public override void Upgrade()
        {
            base.Upgrade();
            Console.WriteLine("A new irrigation system has been installed for the field");
        }

        public void Plant(CropType crop)
        {
            CurrentCrop = crop;
            Console.WriteLine($"Planted in the field: {crop}");
        }
    }
}