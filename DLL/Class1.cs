using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace DLL
{
    public class Class1
    {   
        public double VozrastUser(List <DateTime> ListDat)
        {
            double[] mas = new double[ListDat.Count];
            double Vozrast  = 0;
            int i = 0;
            foreach (DateTime user in ListDat)
            {
                var today = DateTime.Today;

                // Calculate the age.
                mas[i] = today.Year - user.Year;

                // Go back to the year in which the person was born in case of a leap year
                if (user.Date > today.AddYears(Convert.ToInt32(-mas[i]))) mas[i]--; 
                i++;

            }
           
                for (int J = 0; J < mas.Length; J++)
                {
                Vozrast += mas[J];
                }
            Vozrast /= mas.Length;
            return Vozrast;
        }



    }
}
