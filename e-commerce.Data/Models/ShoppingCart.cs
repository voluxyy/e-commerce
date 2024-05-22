namespace ecommerce.Data.Models {
    public class ShoppingCart {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<ProductList>? ProductLists { get; set; }
    }
}