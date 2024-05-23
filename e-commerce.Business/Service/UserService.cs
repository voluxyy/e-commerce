using System.Security.Cryptography;
using System.Text;
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
            var exist = await userRepository.GetByEmail(dto.Email);
            
            if (exist != null)
                throw new ApplicationException("User with this email already exists");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(dto.Password!, out passwordHash, out passwordSalt);

            User user = DtoToModel(dto, passwordHash, passwordSalt);
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
            if (dto.Email == null || dto.Password == null)
                throw new ArgumentNullException("The email and the password are required."); 

            User user = await userRepository.GetByEmail(dto.Email);

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
        }

        private void CreatePasswordHashFromSalt(string password, byte[] salt, out byte[] hash)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}