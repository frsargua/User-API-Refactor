using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Tests.User.Api.Controllers
{
    public class UserController : Controller
    {

        private readonly DatabaseContext _database;
        private readonly ILogger<UserController> _logger;


        public UserController(DatabaseContext database, ILogger<UserController> logger)
        {
            _database = database;
            _logger = logger;

        }

        /// <summary>
        ///     Gets a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/users")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var user = await _database.Users.FindAsync(id);

                if (user == null)
                {
                    return NotFound(new { Message = $"User with ID {id} was not found." });
                }

                return Ok(user);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting user with ID {UserId}.", id);
                return StatusCode(500, new { Message = $"An error occurred while processing your request." });
            }
        }

        /// <summary>
        ///     Create a new user
        /// </summary>
        /// <param name="firstName">First name of the user</param>
        /// <param name="lastName">Last name of the user</param>
        /// <param name="age">Age of the user (must be a number)</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/users")]
        public async Task<IActionResult> Create(string firstName, string lastName, string age)
        {
            try
            {
                var user = new Models.User
                {
                    Age = age,
                    FirstName = firstName,
                    LastName = lastName
                };

                _database.Users.Add(user);
                await _database.SaveChangesAsync();

                return Ok(User);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new user");
                return StatusCode(500, new { Message = $"An error occurred while processing your request." });
            }
           
        }

        /// <summary>
        ///     Updates a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <param name="firstName">First name of the user</param>
        /// <param name="lastName">Last name of the user</param>
        /// <param name="age">Age of the user (must be a number)</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/users")]
        public async Task<IActionResult> Update(int id, string firstName, string lastName, string age)
        {
            try
            {
                var user = await _database.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} was not found." });
            }

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Age = age;

            try
            {
                await _database.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new { Message = $"An error occurred while processing the changes in your request." });
            }

            return Ok();
        }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting user with ID {UserId}.", id);
                return StatusCode(500, new { Message = $"An error occurred while processing your request." });
            }
        }

        /// <summary>
        ///     Delets a user
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/users")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _database.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} was not found." });
            }

            _database.Users.Remove(user);
            await _database.SaveChangesAsync();

            return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting an existing user");
                return StatusCode(500, new { Message = $"An error occurred while processing your request." });
            }
        }
    }
}
