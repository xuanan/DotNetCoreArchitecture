using System;

namespace DotNetCoreArchitecture.Model
{
    public class UserLogEntity
    {
        public UserLogEntity
        (
            long userLogId,
            long userId,
            LogType logType,
            string content,
            DateTime dateTime
        )
        {
            UserLogId = userLogId;
            UserId = userId;
            LogType = logType;
            Content = content;
            DateTime = dateTime;
        }

        public UserLogEntity() { }

        public long UserLogId { get; set; }

        public long UserId { get; set; }

        public LogType LogType { get; set; }

        public string Content { get; set; }

        public DateTime DateTime { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
