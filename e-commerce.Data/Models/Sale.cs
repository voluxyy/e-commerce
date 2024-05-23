namespace ecommerce.Data.Models {
    public class Sale {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public User User { get; set; } = null!;
        public required int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public required string ActivationCode { get; set; }
        public required DateOnly Date { get; set; }
    }
}