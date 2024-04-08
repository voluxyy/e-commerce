namespace ecommerce.Data.Models {
    public class Product {
        public int Id { get; set; }
        public required string ImagePath { get; set; }
        public required string Name { get; set; }
        public required float Price { get; set; }
        public required int Quantity { get; set; }
        public ICollection<ProductList>? ProductLists { get; set; }
    }
}