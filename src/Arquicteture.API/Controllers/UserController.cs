using Arquicteture.API.DTO;
using Arquicteture.API.Model;
using Arquicteture.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Arquicteture.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        /// <summary>
        /// Get user in bd
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("geting")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get(string username)
        {
            var result = await _userRepository.GetUser(username);

            if (result == null)
                return BadRequest("Usuário não cadastrado");

            return Ok(result);
        }
        /// <summary>
        /// Authenticate user in bd
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] AuthDTO model)
        {
            var user = await _userRepository.Authenticate(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);

            return Ok(new
            {
                token = token
            });
        }

        /// <summary>
        /// Creating user in bd
        /// </summary>
        /// <param name="userdto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("creating")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Post([FromBody] UserCreateDTO userdto)
        {
            var user = new User(userdto.Name, userdto.Username, userdto.Password);
          

            if (user.Notifies.Any())
            {
                return BadRequest(user.Notifies);
            }

            var result = await _userRepository.Create(user);

            return Ok(result);
        }

        /// <summary>
        ///  updating user in bd
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updating")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] UserUpdateDTO userDto, string username)
        {
            var user = new User(userDto.Name, username, userDto.Password);

            if (user.Notifies.Any())
            {
                return BadRequest(user.Notifies);
            }

            var result = await _userRepository.Update(user);

            return Ok(result);
        }
        /// <summary>
        /// remove user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleting")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [Authorize]
        public async Task<ActionResult> Delete(string username)
        {
            var result = await _userRepository.Delete(username);
            return Ok(result);
        }
    }
}
