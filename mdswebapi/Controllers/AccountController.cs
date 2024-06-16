using mdswebapi.Dtos.Account;
using mdswebapi.Interfaces;
using mdswebapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mdswebapi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Customer> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<Customer> _signinManager;
        public AccountController(UserManager<Customer> userManager, ITokenService tokenService, SignInManager<Customer> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());

            if (user == null)
            {
                return Unauthorized("Invalid username!");
            }

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found or password incorrect");

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = await _tokenService.CreateToken(user),
                }
                );

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var customer = new Customer
                {
                    UserName = registerDto.CustomerName,
                    Email = registerDto.CustomerEmail,
                };

                var createUser = await _userManager.CreateAsync(customer, registerDto.CustomerPassword);

                if (createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(customer, "User");
                    if(roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto()
                            {
                                UserName = customer.UserName,
                                Email = customer.Email,
                                Token = await _tokenService.CreateToken(customer)
                            }
                            );
                    }else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }else
                {
                    return StatusCode(500, createUser.Errors);
                }
                
            } catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
