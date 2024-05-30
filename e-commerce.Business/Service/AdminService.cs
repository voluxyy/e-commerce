using System.Security.Cryptography;
using System.Text;
using ecommerce.Business.Dto;
using ecommerce.Data.Models;
using ecommerce.Data.Repositories;

namespace ecommerce.Business.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        public async Task<AdminDto> Add(AdminDto dto)
        {
            var exist = await adminRepository.GetByEmail(dto.Email);
            
            if (exist != null)
                throw new ApplicationException("User with this email already exists");

            dto.Id = Guid.NewGuid();

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);

            Admin admin = DtoToModel(dto, passwordHash, passwordSalt);
            await adminRepository.Add(admin);
            AdminDto adminDto = ModelToDto(admin, null!);

            return adminDto;
        }

        public async Task<AdminDto> Update(AdminDto dto)
        {
            Admin currentAdmin = await adminRepository.Get((Guid)dto.Id!);

            currentAdmin.Pseudo = dto.Pseudo;
            currentAdmin.Email = dto.Email;
            currentAdmin.Permission = dto.Permission;

            await adminRepository.Update(currentAdmin);
            AdminDto adminDto = ModelToDto(currentAdmin, null!);

            return adminDto;
        }

        public async Task<AdminDto> UpdatePassword(AdminDto dto)
        {
            Admin currentAdmin = await adminRepository.Get((Guid)dto.Id!);

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);

            currentAdmin.PasswordHash = passwordHash;
            currentAdmin.PasswordSalt = passwordSalt;

            await adminRepository.Update(currentAdmin);
            AdminDto adminDto = ModelToDto(currentAdmin, null!);

            return adminDto;
        }

        public async Task<int> Delete(Guid id)
        {
            return await adminRepository.Delete(id);
        }

        public async Task<AdminDto> Get(Guid id)
        {
            return ModelToDto(await adminRepository.Get(id), null!);
        }

        public List<AdminDto> GetAll()
        {
            List<Admin> admins = adminRepository.GetAll();
            List<AdminDto> adminsDtos = ListModelToDto(admins);
            return adminsDtos;
        }

        public async Task<AdminDto> CheckConnection(LoginDto dto)
        {
            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            {
                throw new ArgumentNullException("Both email and password are required.");
            }

            // Try to get admin by email or pseudo
            Admin admin = await adminRepository.GetByEmail(dto.Email)
                          ?? await adminRepository.GetByPseudo(dto.Email);

            if (admin == null)
            {
                throw new InvalidOperationException("The email or the password provided is wrong.");
            }

            // Validate the password
            byte[] passwordHash;
            CreatePasswordHashFromSalt(dto.Password, admin.PasswordSalt, out passwordHash);

            if (!admin.PasswordHash.SequenceEqual(passwordHash))
            {
                throw new InvalidOperationException("The email or the password provided is wrong.");
            }

            // Convert the admin to AdminDto and return
            return ModelToDto(admin, null);
        }

        // public async Task<AdminDto> CheckConnectionByPseudo(LoginDto dto) {
        //      if (dto.Pseudo == null)
        //         throw new ArgumentNullException("Pseudo can't be null."); 
        //         Console.WriteLine("Error step 1");
        //      if (dto.Password == null)
        //         throw new ArgumentNullException("Password is required.");
        //         Console.WriteLine("Error step 2");

        //     Admin adminPseudo = await adminRepository.GetByPseudo(dto.Pseudo);

        //     if (adminPseudo == null)
        //         throw new InvalidOperationException("Pseudo is wrong or inexistant.");
        //         Console.WriteLine("Error step 3");

        //     byte[] passwordHash;
        //     CreatePasswordHashFromSalt(dto.Password, adminPseudo.PasswordSalt, out passwordHash);
        //     Console.WriteLine("Error step 4");

        //     if (!adminPseudo.PasswordHash.SequenceEqual(passwordHash))
        //         throw new InvalidOperationException("Pseudo provided is wrong.");
        //         Console.WriteLine("Error step 5");

        //     return ModelToDto(adminPseudo, null!);  
        // }

        private List<AdminDto> ListModelToDto(List<Admin> admins)
        {
            List<AdminDto> adminsDtos = new List<AdminDto>();
            foreach (Admin admin in admins)
            {
                adminsDtos.Add(ModelToDto(admin, null!));
            }
            return adminsDtos;
        }

        private AdminDto ModelToDto(Admin admin, string password)
        {
            AdminDto adminDto = new AdminDto
            {
                Id = admin.Id,
                Pseudo = admin.Pseudo,
                Email = admin.Email,
                Password = password,
                Permission = admin.Permission
            };

            return adminDto;
        }

        private Admin DtoToModel(AdminDto adminDto, byte[] passwordHash, byte[] passwordSalt)
        {
            Admin admin = new Admin
            {
                Id = (Guid)adminDto.Id!,
                Pseudo = adminDto.Pseudo,
                Email = adminDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Permission = adminDto.Permission
            };

            return admin;
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