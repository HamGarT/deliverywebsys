using deliveryapp.Models;

namespace deliveryapp.Services
{
    public class ProductService
    {
        private static List<CartItem> _cartItems = new List<CartItem>();

        public List<CartItem> GetAll() => _cartItems;

        public void Add(CartItem product)
        {
            product.Id = _cartItems.Count + 1;
            _cartItems.Add(product);
        }

        public decimal GetTotalAmount()
        {
            decimal totalAmount = 0;
            foreach (var item in _cartItems)
            {
                totalAmount += item.TotalPrice;
            }
          return totalAmount;
        }
        public void ClearCart() { 
            _cartItems.Clear();
        }
    }
}
