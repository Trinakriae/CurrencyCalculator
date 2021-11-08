using CurrencyCalculator.App.Classes;
using System;
using Xunit;

namespace CurrencyCalculator.Test
{
    public class OldBritishPoundUnitTests
    {
        [Fact]
        public void Constructor_InputCorrectFormat_ValuesAreOk()
        {
            var target = new OldBritishPound("12p 2s 10d");

            Assert.True(target.Pound.Integer == 12  && target.Shilling.Integer == 2 && target.Penny.Integer == 10);
            Assert.True(target.Pound.Remainder == 0 && target.Shilling.Remainder == 0 && target.Penny.Remainder == 0);
        }

        [Theory]
        [InlineData("")]
        [InlineData("z")]
        [InlineData("1p")]
        [InlineData("1s")]
        [InlineData("1d")]
        [InlineData("1p 1s")]
        [InlineData("1s 1d")]
        [InlineData("1p 1d")]
        public void Constructor_InputIncorrectFormat_ThrowsFormatException(string input)
        {
            Assert.Throws<FormatException>(() => new OldBritishPound(input));
        }

        [Fact]
        public void Add_InputCorrectFormat_ResultIsOk()
        {
            var target1 = new OldBritishPound("12p 2s 10d");
            target1.Add("1p 17s 1d");

            var target2 = new OldBritishPound("12p 2s 10d");
            target2.Add("1p 20s 12d");

            var target3 = new OldBritishPound("12p 2s 10d");
            target3.Add("1p 50s 37d");

            Assert.True(target1.Pound.Integer == 13 && target1.Shilling.Integer == 19 && target1.Penny.Integer == 11);
            Assert.True(target1.Pound.Remainder == 0 && target1.Shilling.Remainder == 0 && target1.Penny.Remainder == 0);
            Assert.True(target2.Pound.Integer == 14 && target2.Shilling.Integer == 3 && target2.Penny.Integer == 10);
            Assert.True(target2.Pound.Remainder == 0 && target2.Shilling.Remainder == 0 && target2.Penny.Remainder == 0);
            Assert.True(target3.Pound.Integer == 15 && target3.Shilling.Integer == 15 && target3.Penny.Integer == 11);
            Assert.True(target3.Pound.Remainder == 0 && target3.Shilling.Remainder == 0 && target3.Penny.Remainder == 0);
        }

        [Theory]
        [InlineData("")]
        [InlineData("z")]
        [InlineData("1p")]
        [InlineData("1s")]
        [InlineData("1d")]
        [InlineData("1p 1s")]
        [InlineData("1s 1d")]
        [InlineData("1p 1d")]
        public void Add_InputIncorrectFormat_ThrowsFormatException(string input)
        {
            var target = new OldBritishPound("12p 2s 10d");

            Assert.Throws<FormatException>(() => target.Add(input));
        }

        [Fact]
        public void Multiply_InputCorrectFormat_ResultIsOk()
        {
            var target1 = new OldBritishPound("12p 2s 10d");
            target1.Multiply(0);

            var target2 = new OldBritishPound("12p 2s 10d");
            target2.Multiply(1);

            var target3 = new OldBritishPound("12p 2s 10d");
            target3.Multiply(3);

            var target4 = new OldBritishPound("12p 2s 10d");
            target4.Multiply(10);

            Assert.True(target1.Pound.Integer == 0 && target1.Shilling.Integer == 0 && target1.Penny.Integer == 0);
            Assert.True(target1.Pound.Remainder == 0 && target1.Shilling.Remainder == 0 && target1.Penny.Remainder == 0);
            Assert.True(target2.Pound.Integer == 12 && target2.Shilling.Integer == 2 && target2.Penny.Integer == 10);
            Assert.True(target2.Pound.Remainder == 0 && target2.Shilling.Remainder == 0 && target2.Penny.Remainder == 0);
            Assert.True(target3.Pound.Integer == 36 && target3.Shilling.Integer == 8 && target3.Penny.Integer == 6);
            Assert.True(target3.Pound.Remainder == 0 && target3.Shilling.Remainder == 0 && target3.Penny.Remainder == 0);
            Assert.True(target4.Pound.Integer == 121 && target4.Shilling.Integer == 8 && target4.Penny.Integer == 4);
            Assert.True(target4.Pound.Remainder == 0 && target4.Shilling.Remainder == 0 && target4.Penny.Remainder == 0);
        }

        [Fact]
        public void Subtract_InputCorrectFormat_ResultIsOk()
        {
            var target = new OldBritishPound("9p 2s 6d");
            target.Subtract("5p 17s 8d");

            Assert.True(target.Pound.Integer == 3 && target.Shilling.Integer == 4 && target.Penny.Integer == 10);
            Assert.True(target.Pound.Remainder == 0 && target.Shilling.Remainder == 0 && target.Penny.Remainder == 0);
        }

        [Theory]
        [InlineData("")]
        [InlineData("z")]
        [InlineData("1p")]
        [InlineData("1s")]
        [InlineData("1d")]
        [InlineData("1p 1s")]
        [InlineData("1s 1d")]
        [InlineData("1p 1d")]
        public void Subtract_InputIncorrectFormat_ThrowsFormatException(string input)
        {
            var target = new OldBritishPound("12p 2s 10d");

            Assert.Throws<FormatException>(() => target.Subtract(input));
        }

        [Fact]
        public void Divide_InputCorrectFormat_ResultIsOk()
        {
            var target = new OldBritishPound("5p 17s 8d");
            target.Divide(3);

            Assert.True(target.Pound.Integer == 1 && target.Shilling.Integer == 19 && target.Penny.Integer == 2);
            Assert.True(target.Pound.Remainder == 0 && target.Shilling.Remainder == 0 && target.Penny.Remainder == 2);
        }

        [Fact]
        public void Divide_DividerEqualToZero_ThrowsFormatException()
        {
            var target = new OldBritishPound("12p 2s 10d");

            Assert.Throws<ArgumentOutOfRangeException>(() => target.Divide(0));
        }

        [Fact]
        public void Chained_InputCorrectFormat_ResultIsOk()
        {
            var target = new OldBritishPound("5p 17s 8d");
            target.Add("10p 4s 10d")
                .Subtract("5p 40s 8d")
                .Multiply(4)
                .Divide(3);

            Assert.True(target.Pound.Integer == 12 && target.Shilling.Integer == 2 && target.Penny.Integer == 5);
            Assert.True(target.Pound.Remainder == 0 && target.Shilling.Remainder == 0 && target.Penny.Remainder == 1);

        }

        [Fact]
        public void ToString_InputCorrectFormat_ResultIsOk()
        {
            var target = new OldBritishPound("12p 2s 10d");

            Assert.True(String.Equals(target.ToString(), "12p 2s 10d"));
            
        }

        [Fact]
        public void ReAllocateAmounts_InputCorrectFormat_ResultIsOk()
        {
            var target = new OldBritishPound("12p 40s 25d");
            target.ReAllocateAmounts();

            Assert.True(String.Equals(target.ToString(), "14p 2s 1d"));
        }
    }
}
