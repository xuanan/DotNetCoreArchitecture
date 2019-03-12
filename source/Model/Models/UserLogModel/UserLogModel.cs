using System;

namespace DotNetCoreArchitecture.Model
{
    public class UserLogModel
    {
        public UserLogModel(long userId, LogType logType)
        {
            UserId = userId;
            LogType = logType;
        }

        public long UserLogId { get; }

        public long UserId { get; }

        public LogType LogType { get; }

        public string Content { get; }

        public DateTime DateTime { get; }
    }
}
