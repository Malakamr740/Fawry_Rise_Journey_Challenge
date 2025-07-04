namespace Fawry_Rise_Journey_Challenge
{
    public class Product
    {
        public string Name;
        public decimal Price;
        public int Quantity;

        public Product(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
        public bool InStock(int requestedQuantity)
        {
            return Quantity >= requestedQuantity;
        }

        public void DecreaseQuantity(int purchasedQuantity)
        {
            if (Quantity >= purchasedQuantity)
            {
                Quantity -= purchasedQuantity;
            }
            else
            {
                Console.WriteLine("Not enough stock available.");
            }
        }

        public void IncreaseQuantity(int addedQuantity)
        {
            Quantity += addedQuantity;
        }
    }
}
