using CurrencyCalculator.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyCalculator.App.Classes
{

    public class OldBritishPound : ICurrency
    {
        public const int SHILLING_TO_POUND = 20;
        public const int PENNY_TO_SHILLING = 12;

        public OldBritishPoundSection Pound { get; set; }
        public OldBritishPoundSection Shilling { get; set; }
        public OldBritishPoundSection Penny { get; set; }

        public OldBritishPound(string value)
        {

            var subStrings = value.Split(' ');

            this.Pound.Value = Decode(subStrings[0]);
            this.Shilling.Value = Decode(subStrings[1]);
            this.Penny.Value = Decode(subStrings[2]);
        }


        public ICurrency Add(string adder)
        {
            throw new NotImplementedException();
        }

        public ICurrency Divide(int divider)
        {
            throw new NotImplementedException();
        }

        public ICurrency Multiply(int multiplier)
        {
            throw new NotImplementedException();
        }

        public ICurrency Subtract(string subtractor)
        {
            throw new NotImplementedException();
        }

        private int Decode(string toDecode)
        {
            string sanitizedValue = toDecode.Remove(toDecode.Length - 1, 1);
            return Int32.Parse(sanitizedValue);
        }

    }
}
