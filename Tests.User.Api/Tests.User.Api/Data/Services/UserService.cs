using System;
using Microsoft.AspNetCore.Mvc;

namespace Tests.User.Api.Data.Services
{
	public class UserService : IUserService
	{
        private readonly DatabaseContext _database;

        public UserService(DatabaseContext database)
        {
            _database = database;
        }

        public async Task<Models.User> CreateUserAsync( UpdateRequestVM newUser)
        {
            var user = new Models.User
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Age = newUser.Age
            };

            _database.Users.Add(user);
            await _database.SaveChangesAsync();

            return new Models.User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age
            };
        }

        public async Task<Boolean> DeleteUserAsync(int id)
        {
            var user = await _database.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }

            _database.Users.Remove(user);
            await _database.SaveChangesAsync();

            return true;
        }

        public async Task<Models.User> GetUserAsync(int id)
        {
            var user = await _database.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            return new Models.User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age
            };
        }

        public async Task<Models.User> UpdateUserAsync(int id, UpdateRequestVM updatedUser)
        {
            var user = await _database.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Age = updatedUser.Age;

            await _database.SaveChangesAsync();

            return new Models.User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age
            };
        }
    }
}

