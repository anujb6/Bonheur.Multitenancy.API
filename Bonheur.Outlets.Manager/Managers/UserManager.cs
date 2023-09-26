using Bonheur.Outlets.Dataservice.Abstracts.Shops;
using Bonheur.Outlets.Dataservice.Abstracts.Users;
using Bonheur.Outlets.Dataservice.EntityData.Outlets.Models;
using Bonheur.Outlets.Dataservice.Models.Response;
using Bonheur.Outlets.Manager.Exceptions;
using Bonheur.Outlets.Manager.Models.Authetication;
using Bonheur.Outlets.Manager.Models.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Manager.Managers
{
    public class UserManager
    {
        private readonly IUserService _userService;
        private readonly IShopsHelper _shopsHelper;
        public UserManager(IUserService userService, IShopsHelper shopsHelper)
        {
            _userService = userService;
            _shopsHelper = shopsHelper;
        }

        public async Task<Authenticated> AuthenticateUser(Login login)
        {
            var dicValidation = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(login.user_query))
                dicValidation.Add("Email/Username", "Invalid username/email with whitespaces provided");

            if (string.IsNullOrWhiteSpace(login.password))
                dicValidation.Add("Password", "Invalid password profvice with whitespaces");

            if (dicValidation.Count() == 0)
            {
                var user = await _userService.GetUserByEmailOrNameAsync(login.user_query);
                if (user == null)
                {
                    dicValidation.Add("Invalid Argument", "Invalid Email/Username or Password");
                    throw new OutletsDataException(dicValidation);
                }
                var res = _shopsHelper.VerifyPasswordHash(login.password, user.PasswordHash, user.PasswordSalt);
                if (!res)
                {
                    dicValidation.Add("Invalid Argument", "Invalid Email/Username or Password");
                    throw new OutletsDataException(dicValidation);
                }

                var token = _shopsHelper.GenerateJwtToken(user);

                return new Authenticated
                {
                    user = new
                    {
                        shop_id = user.Shop.Id,
                        shop_name = user.Shop.ShopName,
                        user_id = user.Id,
                        user_name = user.Name,
                        user_email = user.Email,
                    },
                    authenticated = true,
                    access_token = token.access_token,
                    refresh_token = token.refresh_token,
                    valid_until = token.valid_until,
                };
            }
            else
                throw new OutletsDataException(dicValidation);
        }

        public async Task<ResponseModel> UpsertUserAsync(CreateUserDTO user)
        {
            var dicValidation = new Dictionary<string, string>();

            if (user.user_id < 0)
                dicValidation.Add("UserId", "Invalid User Id");

            if (user.shop_id <= 0)
                dicValidation.Add("Shop_Id", "Invalid Shop Id");

            if (string.IsNullOrWhiteSpace(user.name))
                dicValidation.Add("UserName", "Invalid User Name remove whitespaces");

            if (string.IsNullOrWhiteSpace(user.email))
                dicValidation.Add("UserEmail", "Invalid User Email remove whitespaces");

            if (string.IsNullOrWhiteSpace(user.password))
                dicValidation.Add("Password", "Invalid Password provided remove whitespaces");

            if (user.phone_no.Count() != 10)
                dicValidation.Add("PhoneNumber", "Invalid Phone number provided");

            if (dicValidation.Count == 0)
            {
                var isNew = user.user_id == 0;
                var userInstance = new User();
                var passwordSalt = _shopsHelper.CreateSalt();
                var passwordHash = _shopsHelper.CreateHash(user.password, passwordSalt);

                if (!isNew)
                    userInstance = await _userService.GetUserById(user.user_id);

                userInstance.ShopId = user.shop_id;
                userInstance.PasswordHash = passwordHash;
                userInstance.PasswordSalt = passwordSalt;
                userInstance.Name = user.name;
                userInstance.PhoneNo = user.phone_no;
                userInstance.Email = user.email;

                if (isNew)
                    return await _userService.AddUserAsync(userInstance);
                else
                    return await _userService.UpdateUserAsync(userInstance);
            }
            else
                throw new OutletsDataException(dicValidation);
        }
    }
}
