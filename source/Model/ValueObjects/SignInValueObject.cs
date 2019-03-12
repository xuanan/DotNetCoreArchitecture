namespace DotNetCoreArchitecture.Model
{
    public sealed class SignInValueObject
    {
        public SignInValueObject(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; }

        public string Password { get; }
    }
}
