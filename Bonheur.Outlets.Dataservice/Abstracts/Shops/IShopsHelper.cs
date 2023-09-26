using Bonheur.Outlets.Dataservice.EntityData.Outlets.Models;
using Bonheur.Outlets.Dataservice.Models.Authetication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Dataservice.Abstracts.Shops
{
    public interface IShopsHelper
    {
        string GetShopConnectionString(IHttpContextAccessor httpContextAccessor);
        byte[] CreateHash(string password, byte[] salt);
        byte[] CreateSalt();
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        string CreateNewAccessTokenFromRefreshToken(string token);
        object IsAccessTokenValid(string token, string secretKey);
        AppTokens GenerateJwtToken(User user);
    }
}
