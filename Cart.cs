namespace Fawry_Rise_Journey_Challenge
{
    public class Cart
    {
        public Dictionary<Product, int> Items { get; set; } // Product and quantity 

        public Cart()   
        {
            Items = new Dictionary<Product, int>();
        }

        public void AddProduct(Product product, int quantity)
        {
            if (product == null)
            {
                Console.WriteLine("Error: Product cannot be null.");
                return;
            }
            if (quantity <= 0)
            {
                Console.WriteLine("Error: Quantity must be greater than zero.");
                return;
            }
            if (!product.InStock(quantity))
            {
                Console.WriteLine($"Error: Not enough stock for {product.Name}. Available: {product.Quantity}");
                return;
            }
            if (product is ExpirableProduct expirableProduct && expirableProduct.IsExpired())
            {
                Console.WriteLine($"Error: {product.Name} is expired.");
                return;
            }

            if (Items.ContainsKey(product))
            {
                int currentCartQuantity = Items[product];
                if (!product.InStock(currentCartQuantity + quantity))
                {
                    Console.WriteLine($"Error: Adding {quantity} to cart would exceed available stock for {product.Name}. Available: {product.Quantity}");
                    return;
                }
                Items[product] += quantity;
            }
            else
            {
                Items.Add(product, quantity);
            }
            Console.WriteLine($"{quantity} x {product.Name} added to cart.");
        }

        public void RemoveProduct(Product product)
        {
            if (Items.ContainsKey(product))
            {
                Items.Remove(product);
                Console.WriteLine($"{product.Name} removed from cart.");
            }
            else
            {
                Console.WriteLine($"{product.Name} not found in cart.");
            }
        }

        public decimal GetCartSubtotal()
        {
            decimal subtotal = 0;
            foreach (var item in Items)
            {
                subtotal += item.Key.Price * item.Value;
            }
            return subtotal;
        }

        public bool IsEmpty()
        {
            return Items.Count == 0;
        }
    }
}
