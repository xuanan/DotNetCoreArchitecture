using DotNetCore.EntityFrameworkCore;
using DotNetCoreArchitecture.Model;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Database
{
    public sealed class UserRepository : EntityFrameworkCoreRelationalRepository<UserEntity>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public Task<SignedInModel> SignInAsync(SignInModel signInModel)
        {
            return SingleOrDefaultAsync<SignedInModel>
            (
                userEntity => userEntity.Login.Equals(signInModel.Login)
                && userEntity.Password.Equals(signInModel.Password)
                && userEntity.Status == Status.Active
            );
        }

        public async Task UpdateStatusAsync(long userId, Status status)
        {
            var userEntity = Select(userId);
            userEntity.Status = status;
            await UpdateAsync(userEntity, userEntity.UserId);
        }
    }
}
