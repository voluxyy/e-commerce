namespace ecommerce.Data.Models {
    public class Rate {
        public int Id { get; set; }
        public int Value { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}