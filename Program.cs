namespace Fawry_Rise_Journey_Challenge
{
    public class Program
    {
       
        static void Main(string[] args)
        {

            //TestCases 
            //-------------------------------------------------------------------
            ECommerceSystem system = new ECommerceSystem();

            // Create a customer
            Customer customer1 = new Customer("Alice", 2000m); 
            Customer customer2 = new Customer("Bob", 50m); 


            Console.WriteLine("--- Scenario 1: Successful Checkout ---");
            Cart cart1 = new Cart();
            cart1.AddProduct(system.GetProductByName("Mobile"), 1);
            cart1.AddProduct(system.GetProductByName("Cheese"), 2); 
            cart1.AddProduct(system.GetProductByName("Laptop"), 1);
            cart1.AddProduct(system.GetProductByName("TV"), 1);
            system.Checkout(customer1, cart1);
            Console.WriteLine($"\nAlice's balance after checkout: {customer1.Balance}");
            Console.WriteLine($"Mobile stock after checkout: {system.GetProductByName("Mobile").Quantity}");
            Console.WriteLine($"Cheese stock after checkout: {system.GetProductByName("Cheese").Quantity}");
            Console.WriteLine($"Laptop stock after checkout: {system.GetProductByName("Laptop").Quantity}");

            // Scenario 2: Empty Cart
            Console.WriteLine("\n--- Scenario 2: Empty Cart ---");
            Cart cart2 = new Cart();
            system.Checkout(customer1, cart2);


            // Scenario 3: Customer Doesn't have Sufficient Balance
            Console.WriteLine("\n--- Scenario 3: Insufficient Balance ---");
            Cart cart3 = new Cart();
            cart3.AddProduct(system.GetProductByName("TV"), 1);
            system.Checkout(customer2, cart3); 


            // Scenario 4: Product Out of Stock
            Console.WriteLine("\n--- Scenario 4: Product Out of Stock ---");
            Cart cart4 = new Cart();
            cart4.AddProduct(system.GetProductByName("TV"), 10); 
            system.Checkout(customer1, cart4);


            // Scenario 5: Expirable Product is Expired (simulated)
            Console.WriteLine("\n--- Scenario 5: Expirable Product is Expired ---");
            ExpirableProduct oldCheese = new ExpirableProduct("Old Cheese", 10m, 5, 3);
            system.GetProductByName("Cheese").Quantity = 0; 
            system._products.Add(oldCheese); 
            Cart cart5 = new Cart();
            cart5.AddProduct(oldCheese, 1);
            system.Checkout(customer1, cart5);


            // Scenario 6: Adding more to cart than available
            Console.WriteLine("\n--- Scenario 6: Adding more to cart than available ---");
            system.GetProductByName("Book").Quantity = 2; 
            Cart cart6 = new Cart();
            cart6.AddProduct(system.GetProductByName("Book"), 1); 
            cart6.AddProduct(system.GetProductByName("Book"), 2); 
            system.Checkout(customer1, cart6); 
            cart6 = new Cart();
            cart6.AddProduct(system.GetProductByName("Book"), 1); 
            cart6.Items[system.GetProductByName("Book")] = 5;
            system.Checkout(customer1, cart6); 


        }
    }
}
