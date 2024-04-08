namespace ecommerce.Business.Dto {
    public class AdminDto {
        public required string Id { get; set; }
        public required string Pseudo { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required int Permission { get; set; }
    }
}