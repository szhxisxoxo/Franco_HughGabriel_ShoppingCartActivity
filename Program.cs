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

    public void DisplayProduct() => Console.WriteLine($"[{Id}] {Name} | P{Price} | Stock: {RemainingStock}");
    public bool HasEnoughStock(int qty) => RemainingStock >= qty;
    public double GetItemTotal(int qty) => Price * qty;
}

public class CartItem
{
    public string Name { get; set; }
    public int Qty { get; set; }
    public double Total { get; set; }
    public CartItem(string n, int q, double t) { Name = n; Qty = q; Total = t; }
}

class Program
{
    static void Main()
    {
        Product[] products = {
            new Product(1, "Paracetamol", 5.00, 100),
            new Product(2, "Ibuprofen", 8.00, 50),
            new Product(3, "Amoxicillin", 15.00, 30),
            new Product(4, "Vitamin C", 12.00, 200),
            new Product(5, "Cetirizine", 10.00, 40),
            new Product(6, "Loperamide", 7.00, 60),
            new Product(7, "Ascorbic Syrup", 120.00, 15),
            new Product(8, "Antacid", 9.00, 80),
            new Product(9, "ORS", 25.00, 25),
            new Product(10, "Face Mask", 150.00, 10)
        };

        CartItem[] cart = new CartItem[10];
        int cartIndex = 0;
        double grandTotal = 0;

        while (true)
        {
            Console.WriteLine("\n--- PHARMACY ---");
            foreach (var p in products) p.DisplayProduct();

            Console.Write("\nEnter ID (0 to checkout): ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id == 0) break;

            Product selected = null;
            foreach (var p in products) if (p.Id == id) selected = p;

            if (selected != null)
            {
                Console.Write($"Quantity for {selected.Name}: ");
                if (int.TryParse(Console.ReadLine(), out int qty) && selected.HasEnoughStock(qty))
                {
                    double itemTotal = selected.GetItemTotal(qty);
                    selected.RemainingStock -= qty;
                    
                    if (cartIndex < cart.Length)
                    {
                        cart[cartIndex++] = new CartItem(selected.Name, qty, itemTotal);
                        grandTotal += itemTotal;
                        Console.WriteLine("Added!");
                    }
                }
                else Console.WriteLine("Invalid qty/stock!");
            }
            else Console.WriteLine("Invalid ID!");
        }

        Console.WriteLine("\n--- RECEIPT ---");
        for (int i = 0; i < cartIndex; i++)
            Console.WriteLine($"{cart[i].Name} x{cart[i].Qty}: P{cart[i].Total}");

        double discount = grandTotal >= 5000 ? grandTotal * 0.10 : 0;
        Console.WriteLine($"\nSubtotal: P{grandTotal}");
        if (discount > 0) Console.WriteLine($"Discount (10%): -P{discount}");
        Console.WriteLine($"Total: P{grandTotal - discount}");
    }
}