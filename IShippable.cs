namespace Fawry_Rise_Journey_Challenge
{
    public interface IShippable
    {
        string GetName();
        decimal GetWeight();
    }

    public class ShippableProduct : Product, IShippable
    {
        public decimal Weight { get; set; }

        public ShippableProduct(string name, decimal price, int quantity, decimal weight)
            : base(name, price, quantity)
        {
            Weight = weight;
        }

        public string GetName()
        {
            return Name;
        }

        public decimal GetWeight()
        {
            return Weight;
        }
    }
}