namespace ecommerce.Data.Models {
    public class User {
        public int Id { get; set; }
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public required string Pseudo { get; set; }
        public required string Email { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public DateOnly? Birthdate { get; set; }
        public required int Money { get; set; }
        public ShoppingCart? ShoppingCarts { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Wish>? Wishs { get; set; }
        public ICollection<Sale>? Sales { get; set; }
    }
}