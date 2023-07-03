using System;
using Microsoft.AspNetCore.Mvc;

namespace Tests.User.Api.Data.Services
{
	public interface IUserService
	{

        Task<Models.User> GetUserAsync(int id);
        Task<Boolean> DeleteUserAsync(int id);
        Task<Models.User> UpdateUserAsync(int id, UpdateRequestVM updatedUser);
        Task<Models.User> CreateUserAsync( UpdateRequestVM newUser);
    }
}

