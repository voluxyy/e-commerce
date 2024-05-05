namespace ecommerce.Business.Dto
{
    public class WishDto
    {
        public int Id { get; set; }
        public required int ProductId { get; set; }
        public required int UserId { get; set; }
    }
}
