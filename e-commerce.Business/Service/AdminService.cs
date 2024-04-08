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
            Admin admin = DtoToModel(dto);
            await adminRepository.Add(admin);
            AdminDto adminDto = ModelToDto(admin);

            return adminDto;
        }

        public async Task<AdminDto> Update(AdminDto dto)
        {
            Admin admin = DtoToModel(dto);
            await adminRepository.Update(admin);
            AdminDto adminDto = ModelToDto(admin);

            return adminDto;
        }

        public async Task<int> Delete(int id)
        {
            return await adminRepository.Delete(id);
        }

        public async Task<AdminDto> Get(int id)
        {
            return ModelToDto(await adminRepository.Get(id));
        }

        public List<AdminDto> GetAll()
        {
            List<Admin> admins = adminRepository.GetAll();
            List<AdminDto> adminsDtos = ListModelToDto(admins);
            return adminsDtos;
        }

        private List<AdminDto> ListModelToDto(List<Admin> admins)
        {
            List<AdminDto> adminsDtos = new List<AdminDto>();
            foreach (Admin admin in admins)
            {
                adminsDtos.Add(ModelToDto(admin));
            }
            return adminsDtos;
        }

        private AdminDto ModelToDto(Admin admin)
        {
            AdminDto adminDto = new AdminDto
            {
                Id = admin.Id,
                Pseudo = admin.Pseudo,
                Email = admin.Email,
                Password = admin.Password,
                Permission = admin.Permission
            };

            return adminDto;
        }

        private Admin DtoToModel(AdminDto adminDto)
        {
            Admin admin = new Admin
            {
                Id = adminDto.Id,
                Pseudo = adminDto.Pseudo,
                Email = adminDto.Email,
                Password = adminDto.Password,
                Permission = adminDto.Permission
            };

            return admin;
        }
    }
}