using Bonheur.Outlets.Dataservice.EntityData.Outlets.Models;
using Bonheur.Outlets.Dataservice.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Dataservice.Abstracts.Users
{
    public interface IUserService
    {
        Task<User?> GetUserByEmailOrNameAsync(string query);

        Task<User> GetUserById(int userId);

        Task<ResponseModel> AddUserAsync(User model);

        Task<ResponseModel> UpdateUserAsync(User user);
    }
}
