namespace ecommerce.Business.Dto {
    public class LoginDto {
        public string? Email { get; set; }
        public string? Pseudo { get; set; }
        public required string Password { get; set; }
        
    }
}