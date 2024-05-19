namespace ecommerce.Data.Models {
    public class Review {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required int Rate { get; set; }
        public required int ProductId { get; set; }
        public required int UserId { get; set; }
    }
}