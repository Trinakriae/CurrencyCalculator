using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyCalculator.App.Classes
{
    public class OldBritishPoundSection
    {
        public int Value { get; set; }
        public int Remainder { get; set; }

        public OldBritishPoundSection()
        {
            Value = 0;
            Remainder = 0;
        }
    }
}
