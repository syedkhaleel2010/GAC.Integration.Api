using AutoMapper;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using GAC.Integration.Infrastructure.Data;
using GAC.Integration.Infrastructure;
using Microsoft.EntityFrameworkCore;
using GAC.Integration.Infrastructure.Repositories;

namespace MG.Marine.Ticketing.SQL.Infrastructure
{
    public class UserRepository :Repository<User>, IUserRepository
    {
        private readonly ServiceDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserRepository(ServiceDbContext dbContext,IUnitOfWork unitOfWork,IMapper mapper) :base(dbContext)
           
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<bool> CreateUser(User entity)
        {
          _dbContext.Users.Add(entity);
            _unitOfWork.Commit();
            return Task.FromResult(true);
        }

        public Task<UserDto> GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExists(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}