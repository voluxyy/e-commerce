namespace ecommerce.Data.Models
{
    public class Wish
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
