namespace DotNetCoreArchitecture.Model
{
    public sealed class FullNameValueObject
    {
        public FullNameValueObject(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public string Name { get; }

        public string Surname { get; }
    }
}
