using Microsoft.Identity.Client;

namespace ecommerce.Data.Models {
    public class ProductList {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ShoppingCartId { get; set; }
    }
}