namespace ecommerce.Data.Models {
    public class Rate {
        public required int Id { get; set; }
        public required int Value { get; set; }
        public required int ProductId { get; set; }
        public required int UserId { get; set; }
    }
}