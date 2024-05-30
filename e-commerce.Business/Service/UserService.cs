using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using ecommerce.Business.Dto;
using ecommerce.Data.Models;
using ecommerce.Data.Repositories;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ecommerce.Business.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public UserService(IUserRepository userRepository, IShoppingCartRepository shoppingCartRepository)
        {
            this.userRepository = userRepository;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<UserDto> Add(UserDto dto)
        {
            string normalizedEmail = dto.Email.ToLowerInvariant();

            ValidateEmail(normalizedEmail);
            ValidatePseudo(dto.Pseudo);
            ValidatePassword(dto.Password);

            var emailExist = await userRepository.GetByEmail(normalizedEmail);
            if (emailExist != null)
                throw new ApplicationException($"{dto.Email} already exists");

            var pseudoExist = await userRepository.GetByPseudo(dto.Pseudo);
            if (pseudoExist != null)
                throw new ApplicationException($"{dto.Pseudo} already exists");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(dto.Password!, out passwordHash, out passwordSalt);

            User user = DtoToModel(dto, passwordHash, passwordSalt);
            user.Email = normalizedEmail;
            await userRepository.Add(user);
            UserDto userDto = ModelToDto(user, null!);

            ShoppingCart shoppingCart = new ShoppingCart
            {
                UserId = userDto.Id,
            };

            await shoppingCartRepository.Add(shoppingCart);

            return userDto;
        }

        public async Task<UserDto> Update(UserDto dto)
        {
            User currentUser = await userRepository.Get(dto.Id);

            currentUser.Lastname = dto.Lastname;
            currentUser.Firstname = dto.Firstname;
            currentUser.Pseudo = dto.Pseudo;
            currentUser.Email = dto.Email;
            currentUser.Birthdate = dto.Birthdate;
            currentUser.Money = dto.Money;

            await userRepository.Update(currentUser);
            UserDto userDto = ModelToDto(currentUser, null!);

            return userDto;
        }

        public async Task<UserDto> UpdatePassword(UserDto dto)
        {
            User currentUser = await userRepository.Get(dto.Id);

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(dto.Password!, out passwordHash, out passwordSalt);

            currentUser.PasswordHash = passwordHash;
            currentUser.PasswordSalt = passwordSalt;

            await userRepository.Update(currentUser);
            UserDto userDto = ModelToDto(currentUser, null!);

            return userDto;
        }

        public async Task<UserDto> UpdateMoney(MoneyDto dto)
        {
            User currentUser = await userRepository.Get(dto.UserId);

            currentUser.Money = dto.Money;

            await userRepository.Update(currentUser);
            UserDto userDto = ModelToDto(currentUser, null!);

            return userDto;
        }

        public async Task<int> Delete(int id)
        {
            return await userRepository.Delete(id);
        }

        public async Task<UserDto> Get(int id)
        {
            return ModelToDto(await userRepository.Get(id), null!);
        }

        public List<UserDto> GetAll()
        {
            List<User> users = userRepository.GetAll();
            List<UserDto> usersDtos = ListModelToDto(users);
            return usersDtos;
        }

        public async Task<UserDto> CheckConnection(LoginDto dto) {
            string email = dto.Email.ToLower();
            if (email == null || dto.Password == null)
                throw new ArgumentNullException("The email and the password are required."); 

            User user = await userRepository.GetByEmail(email);

            if (user == null)
                throw new InvalidOperationException("The email or the password provided is wrong.");

            byte[] passwordHash;
            CreatePasswordHashFromSalt(dto.Password, user.PasswordSalt, out passwordHash);

            if (!user.PasswordHash.SequenceEqual(passwordHash))
                throw new InvalidOperationException("The email or the password provided is wrong.");

            return ModelToDto(user, null!);
        }

        private List<UserDto> ListModelToDto(List<User> users)
        {
            List<UserDto> usersDtos = new List<UserDto>();
            foreach (User user in users)
            {
                usersDtos.Add(ModelToDto(user, null!));
            }
            return usersDtos;
        }

        private UserDto ModelToDto(User user, string password)
        {
            UserDto userDto = new UserDto
            {
                Id = user.Id,
                Lastname = user.Lastname,
                Firstname = user.Firstname,
                Pseudo = user.Pseudo,
                Email = user.Email,
                Password = password,
                Birthdate = user.Birthdate,
                Money = user.Money
            };

            return userDto;
        }

        private User DtoToModel(UserDto userDto, byte[] passwordHash, byte[] passwordSalt)
        {
            User user = new User
            {
                Id = userDto.Id,
                Lastname = userDto.Lastname,
                Firstname = userDto.Firstname,
                Pseudo = userDto.Pseudo,
                Email = userDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Birthdate = userDto.Birthdate,
                Money = userDto.Money
            };

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            /*
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            */
        }

        private void CreatePasswordHashFromSalt(string password, byte[] salt, out byte[] hash)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private void ValidateEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
                throw new ApplicationException("Invalid email format");
        }

        private void ValidatePseudo(string pseudo)
        {
            string pseudoPattern = @"^[a-zA-Z0-9_-]{3,15}$";
            if (!Regex.IsMatch(pseudo, pseudoPattern))
                throw new ApplicationException("Invalid pseudo format. Pseudo must be 3-15 characters long and contain only letters, numbers, underscores, or dashes.");
        }

        private void ValidatePassword(string password)
        {
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            if (!Regex.IsMatch(password, passwordPattern))
                throw new ApplicationException("Invalid password format. Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
        }
    }
}