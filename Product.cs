using System;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int RemainingStock { get; set; }

    public Product(int id, string name, double price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        RemainingStock = stock;
    }

    public void DisplayProduct() => Console.WriteLine($"[{Id}] {Name} | Price: P{Price} | Stock: {RemainingStock}");
    public double GetItemTotal(int qty) => Price * qty;
}