using DotNetCoreArchitecture.Database;
using DotNetCoreArchitecture.Domain;
using DotNetCoreArchitecture.Model;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application
{
    public sealed class UserLogService : BaseService, IUserLogService
    {
        public UserLogService
        (
            IDatabaseUnitOfWork databaseUnitOfWork,
            IUserLogRepository userLogRepository
        )
        {
            DatabaseUnitOfWork = databaseUnitOfWork;
            UserLogRepository = userLogRepository;
        }

        private IDatabaseUnitOfWork DatabaseUnitOfWork { get; }

        private IUserLogRepository UserLogRepository { get; }

        public async Task AddAsync(UserLogModel userLogModel)
        {
            var userLogDomain = UserLogDomainFactory.Create(userLogModel);

            userLogDomain.Add();

            var userLogEntity = UserLogEntityFactory.Create(userLogDomain);

            await UserLogRepository.AddAsync(userLogEntity);

            await DatabaseUnitOfWork.SaveChangesAsync();
        }
    }
}
