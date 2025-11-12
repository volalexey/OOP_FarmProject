using FarmProject.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmProject.Classes
{
    public class CropPlot : Plot
    {
        public CropType CurrentCrop { get; set; }

        public CropPlot(int size, double cost, double income)
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

        public void Plant(CropType crop)
        {
            throw new NotImplementedException();
        }
    }
}
