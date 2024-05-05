namespace ecommerce.Data.Models
{
    public class Wish
    {
        public required int Id { get; set; }
        public required int ProductId { get; set; }
        public required int WishListId { get; set; }
    }
}
