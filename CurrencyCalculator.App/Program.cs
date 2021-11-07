using CurrencyCalculator.App.Classes;
using System;

namespace CurrencyCalculator.App
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var target = new OldBritishPound("5p 17s 8d");
                target.Add("3p 4s 10d");
                Console.WriteLine(target.ToString());

                target = new OldBritishPound("9p 2s 6d");
                target.Subtract("5p 17s 8d");
                Console.WriteLine(target.ToString());

                target = new OldBritishPound("5p 17s 8d");
                target.Multiply(2);
                Console.WriteLine(target.ToString());

                target = new OldBritishPound("18p 16s 1d");
                target.Divide(15);
                Console.WriteLine(target.ToString());

                target = new OldBritishPound("5p 17s 8d");
                target.Divide(3);
                Console.WriteLine(target.ToString());

                target = new OldBritishPound("5p 17s 8d");
                target.Add("10p 4s 10d")
                    .Subtract("5p 40s 8d")
                    .Multiply(4)
                    .Divide(3);
                Console.WriteLine(target.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
