using System;

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
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("[ERROR] Non-numeric input for product number."); continue; }
            if (id == 0) break; 

            Product selected = null;
            foreach (var p in products) { if (p.Id == id) selected = p; }

            if (selected == null) { Console.WriteLine("[ERROR] Invalid product number."); continue; }
            if (selected.RemainingStock == 0) { Console.WriteLine("[ERROR] Item is out-of-stock."); continue; }

            Console.Write($"Enter quantity for {selected.Name}: ");
            if (int.TryParse(Console.ReadLine(), out int qty))
            {
                if (qty > selected.RemainingStock) { Console.WriteLine("[ERROR] Not enough stock available."); }
                else if (qty <= 0) { Console.WriteLine("[ERROR] Invalid quantity."); }
                else
                {
                    double itemTotal = selected.GetItemTotal(qty);
                    selected.RemainingStock -= qty;
                    
                    bool foundInCart = false;
                    for (int i = 0; i < cartIndex; i++)
                    {
                        if (cart[i].ProductId == selected.Id)
                        {
                            cart[i].Qty += qty;
                            cart[i].Total += itemTotal;
                            foundInCart = true;
                            break;
                        }
                    }

                    if (!foundInCart)
                    {
                        if (cartIndex < cart.Length) {
                            cart[cartIndex++] = new CartItem(selected.Id, selected.Name, qty, itemTotal);
                            Console.WriteLine("Added to cart confirmation.");
                        } else {
                            Console.WriteLine("Cart is full.");
                        }
                    }
                    grandTotal += itemTotal;
                }
            }
            else { Console.WriteLine("[ERROR] Non-numeric input for quantity."); }
        }

        if (cartIndex > 0)
        {
            double discount = grandTotal >= 5000 ? grandTotal * 0.10 : 0;
            double finalTotal = grandTotal - discount;

            Console.WriteLine("\n------- FINAL RECEIPT -------");
            for (int i = 0; i < cartIndex; i++) 
                Console.WriteLine($"{cart[i].Name} x{cart[i].Qty}: P{cart[i].Total}");

            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Grand Total: P{grandTotal}");
            if (discount > 0) Console.WriteLine($"Discount amount (10%): -P{discount}");
            Console.WriteLine($"Final Total: P{finalTotal}");

            double payment = 0;
            while (true) 
            {
                Console.Write("\nEnter Payment Amount: P");
                if (double.TryParse(Console.ReadLine(), out payment))
                {
                    if (payment >= finalTotal) 
                    {
                        double change = payment - finalTotal; 
                        Console.WriteLine($"Payment Accepted! Your change: P{change}");
                        break; 
                    }
                    else 
                    {
                        Console.WriteLine($"[ERROR] Insufficient funds. You still owe P{finalTotal - payment}.");
                    }
                }
                else 
                {
                    Console.WriteLine("[ERROR] Invalid input. Please enter a numeric amount.");
                }
            }

            Console.WriteLine("\n--- UPDATED STOCK AFTER CHECKOUT ---");
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Name}: {p.RemainingStock} left");
            }
        }
    }
}