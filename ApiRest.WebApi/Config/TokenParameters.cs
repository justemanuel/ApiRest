using ApiRest.Abstractions;

namespace ApiRest.WebApi.Config
{
    public class TokenParameters : ITokenParameters
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Passwordhash { get; set; }
    }
}
