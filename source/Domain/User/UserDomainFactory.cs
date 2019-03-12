using DotNetCoreArchitecture.Model;

namespace DotNetCoreArchitecture.Domain
{
    public static class UserDomainFactory
    {
        public static UserDomain Create(long userId)
        {
            return new UserDomain(userId);
        }

        public static UserDomain Create(SignInModel signInModel)
        {
            return new UserDomain
            (
                new SignInValueObject
                (
                    signInModel.Login,
                    signInModel.Password
                )
            );
        }

        public static UserDomain Create(SignedInModel signedInModel)
        {
            return new UserDomain
            (
                signedInModel.UserId,
                signedInModel.Roles
            );
        }

        public static UserDomain Create(AddUserModel addUserModel)
        {
            return new UserDomain
            (
                addUserModel.UserId,
                new FullNameValueObject(addUserModel.Name, addUserModel.Surname),
                addUserModel.Email,
                new SignInValueObject(addUserModel.SignIn.Login, addUserModel.SignIn.Password),
                addUserModel.Roles
            );
        }

        public static UserDomain Create(UpdateUserModel updateUserModel)
        {
            return new UserDomain
            (
                updateUserModel.UserId,
                new FullNameValueObject(updateUserModel.Name, updateUserModel.Surname),
                updateUserModel.Email,
                updateUserModel.Roles
            );
        }

        public static UserDomain Create(UserEntity userEntity)
        {
            return new UserDomain
            (
                userEntity.UserId,
                new FullNameValueObject(userEntity.Name, userEntity.Surname),
                userEntity.Email,
                new SignInValueObject(userEntity.Login, userEntity.Password),
                userEntity.Roles
            );
        }
    }
}
