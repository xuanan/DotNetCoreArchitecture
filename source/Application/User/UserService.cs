using DotNetCore.Objects;
using DotNetCoreArchitecture.Database;
using DotNetCoreArchitecture.Domain;
using DotNetCoreArchitecture.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Application
{
    public sealed class UserService : BaseService, IUserService
    {
        public UserService
        (
            IDatabaseUnitOfWork databaseUnitOfWork,
            IUserDomainService userDomainService,
            IUserLogService userLogService,
            IUserRepository userRepository
        )
        {
            DatabaseUnitOfWork = databaseUnitOfWork;
            UserDomainService = userDomainService;
            UserLogService = userLogService;
            UserRepository = userRepository;
        }

        private IDatabaseUnitOfWork DatabaseUnitOfWork { get; }

        private IUserDomainService UserDomainService { get; }

        private IUserLogService UserLogService { get; }

        private IUserRepository UserRepository { get; }

        public async Task<IDataResult<long>> AddAsync(AddUserModel addUserModel)
        {
            var validation = new AddUserModelValidator().Valid(addUserModel);

            if (!validation.Success)
            {
                return ErrorDataResult<long>(validation.Message);
            }

            UserDomainService.GenerateHash(addUserModel.SignIn);

            var userDomain = UserDomainFactory.Create(addUserModel);

            userDomain.Add();

            var userEntity = UserEntityFactory.Create(userDomain);

            await UserRepository.AddAsync(userEntity);

            await DatabaseUnitOfWork.SaveChangesAsync();

            return SuccessDataResult(userEntity.UserId);
        }

        public async Task<IResult> DeleteAsync(long userId)
        {
            await UserRepository.DeleteAsync(userId);

            await DatabaseUnitOfWork.SaveChangesAsync();

            return SuccessResult();
        }

        public async Task<IResult> InactivateAsync(long userId)
        {
            var userDomain = UserDomainFactory.Create(userId);

            userDomain.Inactivate();

            await UserRepository.UpdateStatusAsync(userDomain.UserId, userDomain.Status);

            await DatabaseUnitOfWork.SaveChangesAsync();

            return SuccessResult();
        }

        public async Task<PagedList<UserModel>> ListAsync(PagedListParameters parameters)
        {
            return await UserRepository.ListAsync<UserModel>(parameters);
        }

        public async Task<IEnumerable<UserModel>> ListAsync()
        {
            return await UserRepository.ListAsync<UserModel>();
        }

        public async Task<UserModel> SelectAsync(long userId)
        {
            return await UserRepository.SelectAsync<UserModel>(userId);
        }

        public async Task<IDataResult<SignedInModel>> SignInAsync(SignInModel signInModel)
        {
            var validation = new SignInModelValidator().Valid(signInModel);

            if (!validation.Success)
            {
                return ErrorDataResult<SignedInModel>(validation.Message);
            }

            UserDomainService.GenerateHash(signInModel);

            var signedInModel = await UserRepository.SignInAsync(signInModel);

            validation = new SignedInModelValidator().Valid(signedInModel);

            if (!validation.Success)
            {
                return ErrorDataResult<SignedInModel>(validation.Message);
            }

            await AddUserLogAsync(signedInModel.UserId, LogType.SignIn).ConfigureAwait(false);

            return SuccessDataResult(signedInModel);
        }

        public async Task<IDataResult<TokenModel>> SignInJwtAsync(SignInModel signInModel)
        {
            var result = await SignInAsync(signInModel).ConfigureAwait(false);

            if (!result.Success)
            {
                return ErrorDataResult<TokenModel>(result.Message);
            }

            var token = UserDomainService.GenerateToken(result.Data);

            var tokenModel = new TokenModel(token);

            return SuccessDataResult(tokenModel);
        }

        public async Task SignOutAsync(SignOutModel signOutModel)
        {
            await AddUserLogAsync(signOutModel.UserId, LogType.SignOut).ConfigureAwait(false);
        }

        public async Task<IResult> UpdateAsync(UpdateUserModel updateUserModel)
        {
            var validation = new UpdateUserModelValidator().Valid(updateUserModel);

            if (!validation.Success)
            {
                return ErrorResult(validation.Message);
            }

            var userEntity = await UserRepository.SelectAsync(updateUserModel.UserId);

            var userDomain = UserDomainFactory.Create(userEntity);

            userDomain.Update(updateUserModel);

            userEntity = UserEntityFactory.Create(userDomain);

            await UserRepository.UpdateAsync(userEntity, userEntity.UserId);

            await DatabaseUnitOfWork.SaveChangesAsync();

            return SuccessResult();
        }

        private async Task AddUserLogAsync(long userId, LogType logType)
        {
            var userLogModel = new UserLogModel(userId, logType);

            await UserLogService.AddAsync(userLogModel);
        }
    }
}
