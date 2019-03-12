using System.Collections.Generic;

namespace DotNetCoreArchitecture.Model
{
    public class UserEntity
    {
        public UserEntity
        (
            long userId,
            string name,
            string surname,
            string email,
            string login,
            string password,
            Roles roles,
            Status status
        )
        {
            UserId = userId;
            Name = name;
            Surname = surname;
            Email = email;
            Login = login;
            Password = password;
            Roles = roles;
            Status = status;
        }

        public UserEntity() { }

        public long UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public Roles Roles { get; set; }

        public Status Status { get; set; }

        public virtual ICollection<UserLogEntity> UsersLogs { get; set; }
    }
}
