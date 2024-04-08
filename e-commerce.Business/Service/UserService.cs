using ecommerce.Business.Dto;
using ecommerce.Data.Models;
using ecommerce.Data.Repositories;

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
            User user = DtoToModel(dto);
            await userRepository.Add(user);
            UserDto userDto = ModelToDto(user);

            return userDto;
        }

        public async Task<UserDto> Update(UserDto dto)
        {
            User user = DtoToModel(dto);
            await userRepository.Update(user);
            UserDto userDto = ModelToDto(user);

            return userDto;
        }

        public async Task<int> Delete(int id)
        {
            return await userRepository.Delete(id);
        }

        public async Task<UserDto> Get(int id)
        {
            return ModelToDto(await userRepository.Get(id));
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
                usersDtos.Add(ModelToDto(user));
            }
            return usersDtos;
        }

        private UserDto ModelToDto(User user)
        {
            UserDto userDto = new UserDto
            {
                Id = user.Id,
                Lastname = user.Lastname,
                Firstname = user.Firstname,
                Pseudo = user.Pseudo,
                Email = user.Email,
                Password = user.Password,
                Birthdate = user.Birthdate,
                Money = user.Money
            };

            return userDto;
        }

        private User DtoToModel(UserDto userDto)
        {
            User user = new User
            {
                Id = userDto.Id,
                Lastname = userDto.Lastname,
                Firstname = userDto.Firstname,
                Pseudo = userDto.Pseudo,
                Email = userDto.Email,
                Password = userDto.Password,
                Birthdate = userDto.Birthdate,
                Money = userDto.Money
            };

            return user;
        }
    }
}