namespace Fawry_Rise_Journey_Challenge
{
    public class Customer
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public Customer(string name, decimal balance)
        {
            Name = name;
            Balance = balance;
        }

        public bool HasSufficientBalance(decimal amount)
        {
            return Balance >= amount;
        }

        public void DeductBalance(decimal amount)
        {
            if (HasSufficientBalance(amount))
            {
                Balance -= amount;
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }
    }
}
