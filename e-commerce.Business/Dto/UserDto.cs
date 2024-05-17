namespace ecommerce.Business.Dto {
    public class UserDto {
        public int Id { get; set; }
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public required string Pseudo { get; set; }
        public required string Email { get; set; }
        public string? Password { get; set; }
        public DateOnly? Birthdate { get; set; }
        public int Money { get; set; }
    }
}