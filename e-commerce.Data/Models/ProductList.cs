using System.Runtime.InteropServices;

namespace ecommerce.Data.Models {
    public class ProductList {
        public int Id { get; set; }
        public required int ProductId { get; set; }
        public required int ShoppingCartId { get; set; }
    }
}