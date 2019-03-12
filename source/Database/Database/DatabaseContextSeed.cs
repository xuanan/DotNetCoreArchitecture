using DotNetCoreArchitecture.Model;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreArchitecture.Database
{
    public static class DatabaseContextSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(new UserEntity
            {
                UserId = 1,
                Name = "Administrator",
                Surname = "Administrator",
                Email = "administrator@administrator.com",
                Login = "admin",
                Password = "1h0ATANFe6x7kMHo1PURE74WI0ayevUwfK/+Ie+IWX/xWrFWngcVUwL/ewryn38EMVMQBFaNo4SaVwgXaBWnDw==",
                Roles = Roles.User | Roles.Admin,
                Status = Status.Active
            });
        }
    }
}
