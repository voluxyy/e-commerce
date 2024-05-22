namespace ecommerce.Data.Models
{
    public class Wish
    {
        public int Id { get; set; }
        public required int ProductId { get; set; }
        public required int UserId { get; set; }
    }
}
