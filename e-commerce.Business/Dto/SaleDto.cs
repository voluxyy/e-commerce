namespace ecommerce.Business.Dto {
    public class SaleDto {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required int ProductId { get; set; }
        public string? ActivationCode { get; set; }
        public DateOnly Date { get; set; }
    }
}