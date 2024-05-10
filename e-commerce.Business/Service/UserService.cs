using System.Security.Cryptography;
using System.Text;
using ecommerce.Business.Dto;
using ecommerce.Data.Models;
using ecommerce.Data.Repositories;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ecommerce.Business.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserDto> Add(UserDto dto)
        {
            var exist = await userRepository.GetByEmail(dto.Email);
            
            if (exist != null)
                throw new ApplicationException("User with this email already exists");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);

            User user = DtoToModel(dto, passwordHash, passwordSalt);
            await userRepository.Add(user);
            UserDto userDto = ModelToDto(user, null);

            return userDto;
        }

        public async Task<UserDto> Update(UserDto dto)
        {
            User currentUser = await userRepository.Get(dto.Id);

            User user = DtoToModel(dto, currentUser.PasswordHash, currentUser.PasswordSalt);
            await userRepository.Update(user);
            UserDto userDto = ModelToDto(user, null);

            return userDto;
        }

        public async Task<UserDto> UpdatePassword(UserDto dto) 
        {
            User currentUser = await userRepository.Get(dto.Id);

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);

            User user = DtoToModel(dto, passwordHash, passwordSalt);
            await userRepository.Update(user);
            UserDto userDto = ModelToDto(user, null);

            return userDto;
        }

        public async Task<int> Delete(int id)
        {
            return await userRepository.Delete(id);
        }

        public async Task<UserDto> Get(int id)
        {
            return ModelToDto(await userRepository.Get(id), null);
        }

        public List<UserDto> GetAll()
        {
            List<User> users = userRepository.GetAll();
            List<UserDto> usersDtos = ListModelToDto(users);
            return usersDtos;
        }

        private List<UserDto> ListModelToDto(List<User> users)
        {
            List<UserDto> usersDtos = new List<UserDto>();
            foreach (User user in users)
            {
                usersDtos.Add(ModelToDto(user, null));
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

    }
}