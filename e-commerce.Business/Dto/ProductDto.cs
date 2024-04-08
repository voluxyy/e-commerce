namespace ecommerce.Business.Dto {
    public class ProductDto {
        public int Id { get; set; }
        public required string ImagePath { get; set; }
        public required string Name { get; set; }
        public required float Price { get; set; }
        public required int Quantity { get; set; }
    }
}