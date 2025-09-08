using AutoMapper;
using FluentValidation;
using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using GAC.Integration.Infrastructure;
using GAC.Integration.Service.Interfaces;
using GAC.Integration.Service.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GAC.Integration.Service
{
    public class UserService : BaseService, IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserSession _userSession;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IValidationService _validationService;
        EncryptionService _encryptionService;
        public UserService(ILogger<UserService> logger,
            IUserSession userSession,
            IServiceScopeFactory serviceScopeFactory,
            IMapper mapper,
            IUserRepository userRepository,
            IValidationService validationService) : base(userSession)
        {
            _logger = logger;
            _userSession = userSession;
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
            _userRepository = userRepository;
            _validationService = validationService;
            _encryptionService = new EncryptionService();
        }

        public Task<bool> CreateUser(User entity)
        {
            entity.Password =  _encryptionService.EncryptPswd(entity.Password);
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = "System";

            return _userRepository.CreateUser(entity);

        }
        public async Task<bool> UserLogin(User request)
        {
            request.UserName = request.UserName.ToLower();
            request.Password = _encryptionService.EncryptPswd(request.Password);

            var getallUser = _userRepository.Get(x => x.UserName == request.UserName && x.Password == request.Password);
            if (getallUser == null)
                throw new ArgumentException("Either username or password you have entered is incorrect!!");
            return await Task.FromResult(true);
        }
    }
}
