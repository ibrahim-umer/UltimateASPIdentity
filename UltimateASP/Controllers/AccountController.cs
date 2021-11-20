using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateASP.Data.EntityClasses;
using UltimateASP.Models;
using UltimateASP.RepoOps.IRepo;

namespace UltimateASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly Services.IAuthManager _authManager;
        public AccountController(
            IUnitOfWork unitOfWork, 
            ILogger logger, 
            IMapper mapper,
            UserManager<ApiUser> userManager,
            Services.IAuthManager authManager
            )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            try
            {
                _logger.Information($"Start Registering User via this email: {userDTO.Email}");

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = _mapper.Map<ApiUser>(userDTO);
                user.UserName = userDTO.Email;
                var result = await _userManager.CreateAsync(user, userDTO.Password);
                if (!result.Succeeded)
                {
                    return BadRequest(result);
                }
                await _userManager.AddToRolesAsync(user, userDTO.Roles);
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"some thing wents wrong {nameof(Register)}");
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            try
            {
                _logger.Information($"start login User {loginUserDTO.Email}");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!await _authManager.ValidateUser(loginUserDTO)) return Unauthorized();
                var token = await _authManager.CreateToken();
                return Accepted(new { token = token });
                
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"some thing wents wrong {nameof(Login)}");
                return BadRequest(ex.Message);
            }
        }
    }
}
