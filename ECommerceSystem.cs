namespace Fawry_Rise_Journey_Challenge
{
    public class ECommerceSystem
    {
        public List<Product> _products;
        public ECommerceSystem()
        {
            _products = new List<Product>();

            _products.Add(new Product("Mobile", 500m, 10));
            _products.Add(new Product("TV", 1200m, 5));

            _products.Add(new ExpirableProduct("Cheese", 15.50m, 20, 3));
            _products.Add(new ExpirableProduct("Biscuits", 5.00m, 50, 3));

            _products.Add(new ShippableProduct("Laptop", 900m, 7, 2.5m));
            _products.Add(new ShippableProduct("Book", 20m, 100, 0.8m));    
        }

        public Product GetProductByName(string name)
        {
            foreach (Product product in _products)
            {
                if (product.Name.ToLower().Equals(name.ToLower()))
                {
                    return product;
                }
            }
            return null;

        }

        public void Checkout(Customer customer, Cart cart)
        {
            Console.WriteLine("\n--- Checkout ---");

            //Cart Validation if empty
            if (cart.IsEmpty())
            {
                Console.WriteLine("Error: Cart is empty. Cannot proceed with checkout.");
                return;
            }

            foreach (var item in cart.Items)
            {
                Product product = item.Key;
                int quantityInCart = item.Value;

                //Case someone purchased while this customer was surfing other products
                if (!product.InStock(quantityInCart))
                {
                    Console.WriteLine($"Error: One product is out of stock. {product.Name} has only {product.Quantity} left, but {quantityInCart} requested.");
                    return;
                }

                if (product is ExpirableProduct expirableProduct && expirableProduct.IsExpired())
                {
                    Console.WriteLine($"Error: product is expired. {product.Name} has an expiration date of {expirableProduct.ProductionDate.AddYears(expirableProduct.ExpirationDuration).ToShortDateString()}.");
                    return;
                }
            }

            decimal orderSubtotal = cart.GetCartSubtotal();
            decimal shippingFees = 0;

            List<IShippable> itemsToShip = new List<IShippable>();

            
            foreach (var item in cart.Items)
            {
                if (item.Key is IShippable shippableItem)
                {
                    for (int i = 0; i < item.Value; i++)
                    {
                       
                        itemsToShip.Add(shippableItem);
                    }
                }
            }

            if (itemsToShip.Any())
            {
                shippingFees = ShippingService.CalculateShippingFees(itemsToShip);
            }

            decimal paidAmount = orderSubtotal + shippingFees;

            if (!customer.HasSufficientBalance(paidAmount))
            {
                Console.WriteLine($"Error: Customer's balance is insufficient. \nRequired: {paidAmount}, Available: {customer.Balance}");
                return;
            }

            customer.DeductBalance(paidAmount);

            foreach (var item in cart.Items)
            {
                item.Key.DecreaseQuantity(item.Value);

                Console.WriteLine("\n--- Checkout Details ---");
                Console.WriteLine($"Order Subtotal: {orderSubtotal:C}");
                Console.WriteLine($"Shipping Fees: {shippingFees:C}");
                Console.WriteLine($"Paid Amount: {paidAmount:C}");
                Console.WriteLine($"Customer Current Balance After Payment: {customer.Balance:C}");


                if (itemsToShip.Any())
                {
                    ShippingService.ShipItems(itemsToShip);
                }

                Console.WriteLine("--- Checkout Complete ---");
                cart.Items.Clear();
            }
        }
    } 
}
