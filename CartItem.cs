using System;

namespace PharmacySystem
{
    public class CartItem
    {
        public int ProductId { get; set; } 
        public string Name { get; set; }
        public int Qty { get; set; }
        public double Total { get; set; }

        public CartItem(int id, string n, int q, double t) 
        { 
            ProductId = id; 
            Name = n; 
            Qty = q; 
            Total = t; 
        }
    }
}   