using Bonheur.Outlets.Dataservice.EntityData.Outlets.Models;
using Bonheur.Outlets.Dataservice.EntityData.Outlets;
using Bonheur.Outlets.Dataservice.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bonheur.Outlets.Dataservice.Abstracts.Users;

namespace Bonheur.Outlets.Dataservice.Services
{
    public class UserService : IUserService
    {
        private readonly OutletsContext _outletContext;
        public UserService(OutletsContext outletContext) 
        {
            _outletContext = outletContext;
        }

        public async Task<User?> GetUserByEmailOrNameAsync(string query)
        {
            var shop = await _outletContext.Users
               .Include(x => x.Shop)
               .FirstOrDefaultAsync(x => x.Name == query || x.Email == query);
            return shop;
        }

        public async Task<User> GetUserById(int userId)
        {
            var shop = await _outletContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return shop;
        }

        public async Task<ResponseModel> AddUserAsync(User model)
        {
            model.CreatedOn = DateTime.UtcNow;
            var responseModel = new ResponseModel();
            await _outletContext.AddAsync(model);

            if (_outletContext.SaveChanges() > 0)
            {
                responseModel.Message = "Added User Succesfully";
                responseModel.Status = true;
            }
            else
            {
                responseModel.Message = "Error Adding User";
                responseModel.Status = false;
            }
            return responseModel;
        }

        public async Task<ResponseModel> UpdateUserAsync(User user)
        {
            user.UpdatedOn = DateTime.UtcNow;
            var responseModel = new ResponseModel();

            _outletContext.Update(user);
            if (await _outletContext.SaveChangesAsync() > 0)
            {
                responseModel.Message = "User Updated Succesfully";
                responseModel.Status = true;
            }
            else
            {
                responseModel.Message = "Error Updating User";
                responseModel.Status = false;
            }
            return responseModel;
        }

    }
}
