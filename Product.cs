using System;

namespace PharmacySystem
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int RemainingStock { get; set; }

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
    }
}