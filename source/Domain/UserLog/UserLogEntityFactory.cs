using DotNetCoreArchitecture.Model;

namespace DotNetCoreArchitecture.Domain
{
    public static class UserLogEntityFactory
    {
        public static UserLogEntity Create(UserLogDomain userLogDomain)
        {
            return new UserLogEntity
            (
                userLogDomain.UserLogId,
                userLogDomain.UserId,
                userLogDomain.LogType,
                userLogDomain.Content,
                userLogDomain.DateTime
            );
        }
    }
}
