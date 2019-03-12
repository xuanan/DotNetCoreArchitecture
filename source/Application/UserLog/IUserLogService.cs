using DotNetCoreArchitecture.Model;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application
{
    public interface IUserLogService : IBaseService
    {
        Task AddAsync(UserLogModel userLogModel);
    }
}
