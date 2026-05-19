using System;

namespace PharmacySystem
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Category { get; private set; }
        public double Price { get; private set; }
        public int RemainingStock { get; private set; }

        public Product(int id, string name, string category, double price, int stock)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
            RemainingStock = stock;
        }

        public void DisplayProduct()
        {
            Console.WriteLine($"[{Id}] {Name} ({Category}) | Price: P{Price} | Stock: {RemainingStock}");
        }

        public double GetItemTotal(int qty) => Price * qty;

        public bool ReduceStock(int quantity)
        {
            if (quantity > 0 && quantity <= RemainingStock)
            {
                RemainingStock -= quantity;
                return true;
            }
            return false;
        }

        public void AddStock(int quantity)
        {
            if (quantity > 0)
            {
                RemainingStock += quantity;
            }
        }
    }
}