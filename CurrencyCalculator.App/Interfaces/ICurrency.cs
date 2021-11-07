namespace CurrencyCalculator.App.Interfaces
{
    namespace CurrencyCalculator.Interfaces
    {
        public interface ICurrency
        {
            ICurrency Add(string adder);
            ICurrency Subtract(string subtractor);
            ICurrency Multiply(int multiplier);
            ICurrency Divide(int divider);
            string ToString();
        }
    }

}
