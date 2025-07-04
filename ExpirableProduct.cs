namespace Fawry_Rise_Journey_Challenge
{
    public class ExpirableProduct : Product
    {
        public DateTime ProductionDate { get; set; }
        public int ExpirationDuration { get; set; } // in years

        public ExpirableProduct(string name, decimal price, int quantity, int expirationduration)
                               : base(name, price, quantity)
        {
            ExpirationDuration = expirationduration;
        }

        public bool IsExpired()
        {
            return ProductionDate.Year + ExpirationDuration > DateTime.UtcNow.Year;
        }

    }
}
