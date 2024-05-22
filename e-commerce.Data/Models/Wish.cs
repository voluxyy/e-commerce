namespace ecommerce.Data.Models
{
    public class Wish
    {
        public int Id { get; set; }
        public required int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public required int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
