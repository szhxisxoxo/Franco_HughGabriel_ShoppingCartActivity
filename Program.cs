using System;

namespace PharmacySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Product[] products = {
                new Product(1, "Paracetamol", "Medicine", 5.00, 100),
                new Product(2, "Ibuprofen", "Medicine", 8.00, 50),
                new Product(3, "Vitamin C", "Supplements", 12.00, 4),
                new Product(4, "Face Mask", "Supplies", 150.00, 10),
                new Product(5, "Alcohol", "Supplies", 45.00, 3)
            };

            CartItem[] cart = new CartItem[10]; 
            int cartIndex = 0;
            double grandTotal = 0;

            while (true)
            {
                Console.WriteLine("\n========== PHARMACY SYSTEM ==========");
                Console.WriteLine("[1] View Menu & Add Item");
                Console.WriteLine("[2] Search Product");
                Console.WriteLine("[3] View/Manage Cart");
                Console.WriteLine("[4] Checkout");
                Console.WriteLine("[0] Exit");
                Console.Write("Select Option: ");
                string? mainChoice = Console.ReadLine();

                if (mainChoice == "0") break;

                if (mainChoice == "1")
                {
                    foreach (var p in products) p.DisplayProduct();
                    Console.Write("\nEnter Product ID to add: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        Product? selected = null;
                        foreach (var p in products) if (p.Id == id) selected = p;

                        if (selected == null) Console.WriteLine("[ERROR] Invalid ID.");
                        else if (selected.RemainingStock <= 0) Console.WriteLine("[ERROR] Out of stock.");
                        else
                        {
                            Console.Write("Enter quantity: ");
                            if (int.TryParse(Console.ReadLine(), out int qty) && qty > 0 && qty <= selected.RemainingStock)
                            {
                                double total = selected.GetItemTotal(qty);
                                cart[cartIndex++] = new CartItem(selected.Id, selected.Name, qty, total);
                                selected.RemainingStock -= qty;
                                grandTotal += total;
                                Console.WriteLine("Added to cart!");
                            }
                            else Console.WriteLine("[ERROR] Invalid quantity or not enough stock.");
                        }
                    }
                }
                else if (mainChoice == "2")
                {
                    Console.Write("Enter product name to search: ");
                    string search = Console.ReadLine()?.ToLower() ?? "";
                    foreach (var p in products)
                        if (p.Name.ToLower().Contains(search)) p.DisplayProduct();
                }
                else if (mainChoice == "3")
                {
                    ManageCart(cart, ref cartIndex, ref grandTotal, products);
                }
                else if (mainChoice == "4")
                {
                    if (cartIndex == 0) Console.WriteLine("[ERROR] Cart is empty.");
                    else PerformCheckout(cart, ref cartIndex, ref grandTotal, products);
                }
            }
        }

        static void ManageCart(CartItem[] cart, ref int cartIndex, ref double grandTotal, Product[] products)
        {
            while (true)
            {
                Console.WriteLine("\n--- CURRENT CART ---");
                for (int i = 0; i < cartIndex; i++)
                    Console.WriteLine($"{i + 1}. {cart[i].Name} | Qty: {cart[i].Qty} | P{cart[i].Total}");
                
                Console.WriteLine("--------------------");
                Console.WriteLine("[1] Remove Item [2] Clear Cart [0] Back");
                Console.Write("Choice: ");
                string? choice = Console.ReadLine();

                if (choice == "0") break;
                if (choice == "1")
                {
                    Console.Write("Enter line number to remove: ");
                    if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= cartIndex)
                    {
                        foreach(var p in products) if(p.Id == cart[idx-1].ProductId) p.RemainingStock += cart[idx-1].Qty;
                        
                        grandTotal -= cart[idx-1].Total;
                        for (int i = idx - 1; i < cartIndex - 1; i++) cart[i] = cart[i + 1];
                        cartIndex--;
                        Console.WriteLine("Item removed.");
                    }
                }
                else if (choice == "2") { cartIndex = 0; grandTotal = 0; Console.WriteLine("Cart cleared."); break; }
            }
        }

        static void PerformCheckout(CartItem[] cart, ref int cartIndex, ref double grandTotal, Product[] products)
        {
            double discount = grandTotal >= 5000 ? grandTotal * 0.10 : 0;
            double finalTotal = grandTotal - discount;

            Console.WriteLine($"Final Total: P{finalTotal}");
            
            double payment = 0;
            while (true)
            {
                Console.Write("Enter payment amount: ");
                if (double.TryParse(Console.ReadLine(), out payment) && payment >= finalTotal) break;
                Console.WriteLine("[ERROR] Insufficient or invalid payment.");
            }

            Console.WriteLine("\n======= OFFICIAL RECEIPT =======");
            Console.WriteLine($"Receipt No: {new Random().Next(1000, 9999)}");
            Console.WriteLine($"Date: {DateTime.Now:f}");
            Console.WriteLine("--------------------------------");
            for (int i = 0; i < cartIndex; i++) Console.WriteLine($"{cart[i].Name} x{cart[i].Qty}");
            Console.WriteLine($"Grand Total: P{grandTotal}");
            Console.WriteLine($"Change: P{payment - finalTotal}");
            Console.WriteLine("================================\n");

            Console.WriteLine("LOW STOCK ALERT:");
            foreach (var p in products)
                if (p.RemainingStock <= 5) Console.WriteLine($"{p.Name} only has {p.RemainingStock} left!");

            cartIndex = 0;
            grandTotal = 0;
        }
    }
}