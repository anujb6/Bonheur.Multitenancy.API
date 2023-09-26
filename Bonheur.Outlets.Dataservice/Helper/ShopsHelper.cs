using Bonheur.Outlets.Dataservice.Abstracts.Shops;
using Bonheur.Outlets.Dataservice.EntityData.Outlets.Models;
using Bonheur.Outlets.Dataservice.Models.Authetication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Dataservice.Helper
{
    public class ShopsHelper : IShopsHelper
    {
        private readonly IConfiguration _configuration;
        public ShopsHelper(IConfiguration cofiguration)
        {
            _configuration = cofiguration;
        }

        public string GetShopConnectionString(IHttpContextAccessor httpContextAccessor)
        {
            var connectionString = _configuration.GetConnectionString("TenantDatabase");
            var shop_name = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("shop_name"))?.Value;
            connectionString = connectionString.Replace("dbname", shop_name);
            return connectionString;
        }

        public byte[] CreateHash(string password, byte[] salt)
        {
            using var sha256 = SHA256.Create();
            var saltedPassword = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray();

            return sha256.ComputeHash(saltedPassword);
        }

        public byte[] CreateSalt()
        {
            var salt = new byte[16];
            using var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(salt);

            return salt;
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var computedHash = CreateHash(password, passwordSalt);

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i])
                {
                    return false;
                }
            }
            return true;
        }

        public string CreateNewAccessTokenFromRefreshToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var ShopId = jwtToken.Claims.FirstOrDefault(c => c.Type == "Shop_Id").Value;
            var UserName = jwtToken.Claims.FirstOrDefault(c => c.Type == "Username").Value;
            var UserEmail = jwtToken.Claims.FirstOrDefault(c => c.Type == "Useremail").Value;
            var UserId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId").Value;

            List<Claim> claims = new List<Claim>
            {
                new Claim("Shop_Id", ShopId),
                new Claim("UserId", UserId),
                new Claim("Username", UserName),
                new Claim("Useremail", UserEmail)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Secret").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var access_token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(5),
                signingCredentials: cred
             );

            return new JwtSecurityTokenHandler().WriteToken(access_token);

        }

        public object IsAccessTokenValid(string token, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (IsTokenExpired(token))
            {
                try
                {
                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                        ClockSkew = TimeSpan.Zero,
                        ValidateLifetime = false,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                    };

                    var ex = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                    return new { message = "Token is valid and Expired", status = true };
                }
                catch (SecurityTokenException)
                {
                    return new { message = "Invalid Token", status = false };
                }
            }
            else
                return new { message = "Token has not expired", status = false };
        }

        public bool IsTokenExpired(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var expiration = jwtToken.ValidTo;

            var isExpired = DateTime.UtcNow > expiration;

            return isExpired;
        }

        public AppTokens GenerateJwtToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("shop_id", user.Id.ToString()),
                new Claim("user_id", user.ShopId.ToString()),
                new Claim("shop_name", user.Shop.ShopName),
                new Claim("user_name", user.Name),
                new Claim("user_email", user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Secret").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var expirationTime = DateTime.UtcNow.AddHours(1);

            var access_token = new JwtSecurityToken(
                claims: claims,
                expires: expirationTime,
                signingCredentials: cred
            );

            var refresh_token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(20),
                signingCredentials: cred
            );

            return new AppTokens
            {
                refresh_token = new JwtSecurityTokenHandler().WriteToken(refresh_token),
                access_token = new JwtSecurityTokenHandler().WriteToken(access_token),
                valid_until = expirationTime
            };
        }
    }
}
