namespace ecommerce.Business.Dto {
    public class UserDto {
        public int Id { get; set; }
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public required string Pseudo { get; set; }
        public required string Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }

        public DateOnly? Birthdate { get; set; }
        public required int Money { get; set; }
    }
}