namespace ecommerce.Data.Models {
    public class Admin {
        public Guid Id { get; set; }
        public required string Pseudo { get; set; }
        public required string Email { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public required int Permission { get; set; }
    }
}