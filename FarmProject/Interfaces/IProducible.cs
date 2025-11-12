using System;
using System.Collections.Generic;
using System.Text;

namespace FarmProject.Interfaces
{
    public interface IProducible
    {
        double CalculateIncome();
        void Upgrade();
    }
}
