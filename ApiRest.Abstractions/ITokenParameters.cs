namespace ApiRest.Abstractions
{
    public interface ITokenParameters
    {
        string Id { get; set; }
        string UserName { get; set; }
        string Passwordhash { get; set; }
    }
}
