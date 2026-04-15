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

public class CartItem
{
    public string Name { get; set; }
    public int Qty { get; set; }
    public double Total { get; set; }
    public CartItem(string n, int q, double t) { Name = n; Qty = q; Total = t; }
}

class Program
{
    static void Main(string[] args)
    {
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

        CartItem[] cart = new CartItem[10]; 
        int cartIndex = 0;
        double grandTotal = 0;

        while (true)
        {
            Console.WriteLine("\n========== PHARMACY MENU ==========");
            foreach (var p in products) p.DisplayProduct();
            Console.WriteLine("===================================");

            Console.Write("\nEnter Product ID (0 to checkout): ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id == 0) break;

            Product selected = null;
            foreach (var p in products) { if (p.Id == id) selected = p; }

            if (selected != null)
            {
                Console.Write($"How many {selected.Name}? ");
                if (int.TryParse(Console.ReadLine(), out int qty))
                {
                    // --- THE STOCK CHECK LOGIC ---
                    if (qty > selected.RemainingStock)
                    {
                        Console.WriteLine($"\n[ERROR] Not enough stock! We only have {selected.RemainingStock} items left.");
                    }
                    else if (qty <= 0)
                    {
                        Console.WriteLine("\n[ERROR] Please enter a quantity greater than 0.");
                    }
                    else
                    {
                        // Success path
                        double itemTotal = selected.GetItemTotal(qty);
                        selected.RemainingStock -= qty;
                        
                        if (cartIndex < cart.Length) {
                            cart[cartIndex++] = new CartItem(selected.Name, qty, itemTotal);
                            grandTotal += itemTotal;
                            Console.WriteLine("Added to cart!");
                        }
                    }
                }
            }
            else { Console.WriteLine("Invalid ID!"); }
        }

        // Receipt & Discount
        Console.WriteLine("\n------- FINAL RECEIPT -------");
        for (int i = 0; i < cartIndex; i++) 
            Console.WriteLine($"{cart[i].Name} x{cart[i].Qty}: P{cart[i].Total}");

        double discount = grandTotal >= 5000 ? grandTotal * 0.10 : 0;
        Console.WriteLine("-----------------------------");
        Console.WriteLine($"Subtotal: P{grandTotal}");
        if (discount > 0) Console.WriteLine($"Discount (10%): -P{discount}");
        Console.WriteLine($"Total Amount Due: P{grandTotal - discount}");
    }
}