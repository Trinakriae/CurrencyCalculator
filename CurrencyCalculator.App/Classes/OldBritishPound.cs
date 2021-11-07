using CurrencyCalculator.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
            ValidateFormat(value);

            var subStrings = value.Split(' ');

            this.Pound = new OldBritishPoundSection(Decode(subStrings[0]));
            this.Shilling = new OldBritishPoundSection(Decode(subStrings[1]));
            this.Penny = new OldBritishPoundSection(Decode(subStrings[2]));
        }


        public ICurrency Add(string adder)
        {
            var adderObj = new OldBritishPound(adder);

            this.Penny.Integer += adderObj.Penny.Integer;

            this.Shilling.Integer += adderObj.Shilling.Integer;

            this.Pound.Integer += adderObj.Pound.Integer;

            return ReAllocateAmounts();
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

        private void ValidateFormat(string value)
        {
            Regex rx = new Regex(@"[0-9]+p[ 0-9]+s[ 0-9]+d",
                RegexOptions.Compiled);

            if (rx.Matches(value).Count != 1)
            {
                throw new FormatException($"The parameter {value} is not in the correct format <valueInPound>p <valueInShelling>s <valueInPenny>d");
            }
        }

        public ICurrency ReAllocateAmounts()
        {
            int lentShilling = 0;
            int lentPound = 0;

            if (this.Penny.Integer >= PENNY_TO_SHILLING)
            {
                lentShilling += (int)Math.Floor((decimal)this.Penny.Integer / PENNY_TO_SHILLING);
                this.Penny.Integer %= PENNY_TO_SHILLING;
            }

            this.Shilling.Integer += lentShilling;

            if (this.Shilling.Integer >= SHILLING_TO_POUND)
            {
                lentPound += (int)Math.Floor((decimal)this.Shilling.Integer / SHILLING_TO_POUND);
                this.Shilling.Integer %= SHILLING_TO_POUND;
            }

            this.Pound.Integer += lentPound;

            return this;
        }

    }
}
