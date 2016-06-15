namespace cerealize.common.Objects.Contracts.Strings
{
    public interface IDuplexString : IBaseObject
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}