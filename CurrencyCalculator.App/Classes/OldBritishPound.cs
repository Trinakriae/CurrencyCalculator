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
            int toDivideShilling = 0;
            int toDividePenny = 0;
            int remainderPound = 0;
            int remainderShilling = 0;

            remainderPound = this.Pound.Integer % divider;
            this.Pound.Integer = (int)Math.Floor((decimal)(this.Pound.Integer / divider));

            toDivideShilling = (this.Shilling.Integer + remainderPound * SHILLING_TO_POUND);
            remainderShilling = toDivideShilling  % divider;
            this.Shilling.Integer = (int)Math.Floor((decimal)toDivideShilling / divider);

            toDividePenny = (this.Penny.Integer + remainderShilling * PENNY_TO_SHILLING);
            this.Penny.Remainder = toDividePenny % divider;
            this.Penny.Integer = (int)Math.Floor((decimal)toDividePenny / divider);

            return ReAllocateAmounts();
        }

        public ICurrency Multiply(int multiplier)
        {
            this.Penny.Integer *= multiplier;

            this.Shilling.Integer *= multiplier;

            this.Pound.Integer *= multiplier;

            return ReAllocateAmounts();
        }

        public ICurrency Subtract(string subtractor)
        {
            int borrowedShilling = 0;
            int borrowedPound = 0;

            var subtractorObj = new OldBritishPound(subtractor);

            var pennySub = this.Penny.Integer - subtractorObj.Penny.Integer; 

            if (pennySub > 0)
            {
                this.Penny.Integer = pennySub;
            }
            else
            {
                borrowedShilling = (int)Math.Ceiling((decimal)Math.Abs(pennySub) / PENNY_TO_SHILLING);
                this.Penny.Integer = pennySub + (borrowedShilling * PENNY_TO_SHILLING);
            }

            var shillingSub = this.Shilling.Integer - subtractorObj.Shilling.Integer - borrowedShilling;

            if (shillingSub > 0)
            {
                this.Shilling.Integer = shillingSub;
            }
            else
            {
                borrowedPound = (int)Math.Ceiling((decimal)Math.Abs(shillingSub) / SHILLING_TO_POUND);
                this.Shilling.Integer = shillingSub + (borrowedPound * SHILLING_TO_POUND);
            }

            this.Pound.Integer -= (subtractorObj.Pound.Integer + borrowedPound);

            return this;
        }

        public override string ToString()
        {
            var integer = $"{this.Pound.Integer}p {this.Shilling.Integer}s {this.Penny.Integer}d";
            var remainder = string.Empty;

            if (this.Pound.Remainder != 0 || this.Shilling.Remainder != 0 || this.Penny.Remainder != 0)
            {
                remainder += "(";
                remainder += this.Pound.Remainder != 0 ? this.Pound.Remainder + "p " : string.Empty;
                remainder += this.Shilling.Remainder != 0 ? this.Shilling.Remainder + "s " : string.Empty;
                remainder += this.Penny.Remainder != 0 ? this.Penny.Remainder + "d" : string.Empty;
                remainder = remainder.Trim();
                remainder += ")";
            }

            return $"{integer} {remainder}".Trim();
        }

        public ICurrency ReAllocateAmounts()
        {
            int lentShilling = 0;
            int lentPound = 0;

            if (this.Penny.Integer >= PENNY_TO_SHILLING)
            {
                lentShilling = (int)Math.Floor((decimal)this.Penny.Integer / PENNY_TO_SHILLING);
                this.Penny.Integer %= PENNY_TO_SHILLING;
            }

            this.Shilling.Integer += lentShilling;

            if (this.Shilling.Integer >= SHILLING_TO_POUND)
            {
                lentPound = (int)Math.Floor((decimal)this.Shilling.Integer / SHILLING_TO_POUND);
                this.Shilling.Integer %= SHILLING_TO_POUND;
            }

            this.Pound.Integer += lentPound;

            lentShilling = 0;
            lentPound = 0;

            if (this.Penny.Remainder >= PENNY_TO_SHILLING)
            {
                lentShilling = (int)Math.Floor((decimal)this.Penny.Remainder / PENNY_TO_SHILLING);
                this.Penny.Remainder %= PENNY_TO_SHILLING;
            }

            this.Shilling.Remainder += lentShilling;

            if (this.Shilling.Remainder >= SHILLING_TO_POUND)
            {
                lentPound = (int)Math.Floor((decimal)this.Shilling.Remainder / SHILLING_TO_POUND);
                this.Shilling.Remainder %= SHILLING_TO_POUND;
            }

            this.Pound.Remainder += lentPound;


            return this;
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
    }
}
