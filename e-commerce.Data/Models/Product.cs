namespace ecommerce.Data.Models {
    public class Product {
        public int Id { get; set; }
        public required string ImagePath { get; set; }
        public required string Name { get; set; }
        public required float Price { get; set; }
        public required int Quantity { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<ProductList>? ProductLists { get; set; }
        public ICollection<Wish>? Wishs { get; set; }
        public ICollection<Sale>? Sales { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}