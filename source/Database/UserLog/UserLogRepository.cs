using DotNetCore.EntityFrameworkCore;
using DotNetCoreArchitecture.Model;

namespace DotNetCoreArchitecture.Database
{
    public sealed class UserLogRepository : EntityFrameworkCoreRelationalRepository<UserLogEntity>, IUserLogRepository
    {
        public UserLogRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
