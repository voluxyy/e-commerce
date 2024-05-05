namespace ecommerce.Data.Models {
    public class User {
        public required int Id { get; set; }
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public required string Pseudo { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateOnly? Birthdate { get; set; }
        public required int Money { get; set; }
        public ICollection<ShoppingCart>? ShoppingCarts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Rate>? Rates { get; set; }
        public ICollection<Wish>? Wishs { get; set; }
        public ICollection<Sale>? Sales { get; set; }
    }
}