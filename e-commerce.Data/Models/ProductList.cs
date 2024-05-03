using System.Runtime.InteropServices;

namespace ecommerce.Data.Models {
    public class ProductList {
        public required int Id { get; set; }
        public required int ProductId { get; set; }
        public required int ShoppingCartId { get; set; }
    }
}