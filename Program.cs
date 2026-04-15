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

    public void DisplayProduct()
    {
        Console.WriteLine($"[{Id}] {Name} | Price: P{Price} | Stock: {RemainingStock}");
    }

    public bool HasEnoughStock(int quantity)
    {
        return RemainingStock >= quantity;
    }

    public double GetItemTotal(int quantity)
    {
        return Price * quantity;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // 1. Your Inventory
        Product[] products = {
            new Product(1, "Paracetamol (500mg)", 5.00, 100),
            new Product(2, "Ibuprofen (200mg)", 8.00, 50),
            new Product(3, "Amoxicillin (500mg)", 15.00, 30),
            new Product(4, "Vitamin C", 12.00, 200),
            new Product(5, "Cetirizine", 10.00, 40),
            new Product(6, "Loperamide", 7.00, 60),
            new Product(7, "Ascorbic Acid Syrup", 120.00, 15),
            new Product(8, "Antacid Tablet", 9.00, 80),
            new Product(9, "Oral Rehydration Salts", 25.00, 25),
            new Product(10, "Face Mask (Box of 50)", 150.00, 10)
        };

        // 2. Display the Menu
        Console.WriteLine("========== PHARMACY MENU ==========");
        foreach (Product p in products)
        {
            p.DisplayProduct();
        }
        Console.WriteLine("===================================\n");

        // 3. Simple Interaction
        Console.Write("Enter Product ID to buy (or 0 to cancel): ");
        string input = Console.ReadLine() ?? "";

        if (int.TryParse(input, out int selectedId))
        {
            if (selectedId == 0)
            {
                Console.WriteLine("Thank you for visiting!");
            }
            else
            {
                Console.WriteLine($"You picked item #{selectedId}. Let's check the stock...");
            }
        }
            else
            {
                Console.WriteLine("Error: Please enter a valid number ID.");
            }
        }
    }