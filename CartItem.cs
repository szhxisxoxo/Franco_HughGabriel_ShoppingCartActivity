namespace PharmacySystem
{
    public class CartItem
    {
        public int ProductId { get; private set; } 
        public string Name { get; private set; }
        public int Qty { get; private set; }
        public double Total { get; private set; }

        public CartItem(int id, string name, int qty, double total) 
        { 
            ProductId = id; 
            Name = name; 
            Qty = qty; 
            Total = total; 
        }
    }
}