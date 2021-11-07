using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyCalculator.App.Classes
{
    public class OldBritishPoundSection
    {
        public int Integer { get; set; }
        public int Remainder { get; set; }

        public OldBritishPoundSection(int integer, int remainder = 0)
        {
            Integer = integer;
            Remainder = remainder;
        }
    }
}
