namespace Fawry_Rise_Journey_Challenge
{
    public class ShippingService
    {
       private const decimal RatePerKg = 5.0m; // Example rate

        public static decimal CalculateShippingFees(List<IShippable> shippableItems)
        {
            decimal totalWeight = 0;
            foreach (var item in shippableItems)
            {
                totalWeight += item.GetWeight();
            }
            return (decimal)totalWeight * RatePerKg;
        }

        public static void ShipItems(List<IShippable> shippableItems)
        {
            if (shippableItems == null || shippableItems.Count == 0)
            {
                Console.WriteLine("No items to ship.");
                return;
            }

            Console.WriteLine("\n--- Sending items to ShippingService ---");
            foreach (var item in shippableItems)
            {
                Console.WriteLine($"Shipping: {item.GetName()} (Weight: {item.GetWeight()} kg)");
            }
            Console.WriteLine("--- Shipping request sent ---");
        }
    }
}
