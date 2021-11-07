using CurrencyCalculator.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyCalculator.App.Classes
{

    public class OldBritishPound : ICurrency
    {
        public OldBritishPound(string value)
        {

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
    }
}
