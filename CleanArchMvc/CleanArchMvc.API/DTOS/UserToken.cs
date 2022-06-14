using System;

namespace CleanArchMvc.API.DTOS
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
