using DotNetCore.Validation;
using FluentValidation;

namespace DotNetCoreArchitecture.Model
{
    public sealed class AddUserModelValidator : Validator<AddUserModel>
    {
        public AddUserModelValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Surname).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.SignIn).NotNull().NotEmpty();
            RuleFor(x => x.SignIn.Login).NotNull().NotEmpty();
            RuleFor(x => x.SignIn.Password).NotNull().NotEmpty();
        }
    }
}
