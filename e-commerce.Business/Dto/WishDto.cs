namespace ecommerce.Business.Dto
{
    public class WishDto
    {
        public required int Id { get; set; }
        public required int ProductId { get; set; }
        public required int WishListId { get; set; }
    }
}
