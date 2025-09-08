using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using GAC.Integration.Infrastructure.IRepositories;

namespace GAC.Integration.Infrastructure
{
    public interface IUserRepository :IRepository<User>
    {
        Task<bool> CreateUser(User entity);
        Task<UserDto> GetUserById(Guid id);
        Task<IEnumerable<UserDto>> GetUsers();
        Task<bool> UserExists(Guid id);
    }
}
