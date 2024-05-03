namespace ecommerce.Data.Models {
    public class Comment {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int ProductId { get; set; }
        public required int UserId { get; set; }
    }
}