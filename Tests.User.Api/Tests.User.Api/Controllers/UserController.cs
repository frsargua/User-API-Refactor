using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tests.User.Api.Data;
using Tests.User.Api.Data.Services;

namespace Tests.User.Api.Controllers
{
    public class UserController : Controller
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;


        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;

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
                var user = await _userService.GetUserAsync(id);

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
        public async Task<IActionResult> Create([FromQuery] UpdateRequestVM newUser)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userService.CreateUserAsync(newUser);

                if (user == null)
                {
                    return StatusCode(500, new { Message = $"An error occurred while creating the user." });
                }

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
        public async Task<IActionResult> Update(int id, [FromQuery] UpdateRequestVM updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userService.UpdateUserAsync(id, updatedUser);

                if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} was not found." });
            }

            return Ok(user);
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
                var result = await _userService.DeleteUserAsync(id);

                if (!result)
                {
                    return NotFound(new { Message = $"User with ID {id} was not found." });
                }

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
