namespace ecommerce.Business.Dto {
    public class ProductListDto {
        public int Id { get; set; }
        public required int ProductId { get; set; }
        public required int ShoppingCartId { get; set; }
    }
}