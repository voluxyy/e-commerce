namespace ecommerce.Business.Dto {
    public class ReviewDto {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required int Rate { get; set; }
        public required int ProductId { get; set; }
        public required int UserId { get; set; }
    }
}