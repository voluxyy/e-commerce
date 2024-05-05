namespace ecommerce.Business.Dto {
    public class RateDto {
        public required int Id { get; set; }
        public required int UserId { get; set; }
        public required int ProductId { get; set; }
        public required int ActivationCode { get; set; }
    }
}