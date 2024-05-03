namespace ecommerce.Data.Models
{
    public class Category
    {
        public required int Id { get; set; }
        public required string CategoryName { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
