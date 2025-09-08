using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAC.Integration.Service.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(User entity);
        Task<bool> UserLogin(User request);
    }
}
