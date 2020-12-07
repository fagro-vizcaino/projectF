using ProjectF.Application.Auth;
using Microsoft.AspNetCore.Mvc;
using LanguageExt;
using ProjectF.Data.Entities.Auth;
using static ProjectF.Data.Entities.Auth.UserMapper;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using ProjectF.EmailService;
using System;
using System.Text;

namespace ProjectF.Api.Features.Auth
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        readonly UserManager<User> _userManager;
        readonly AuthUserCrudHandler _authUser;
        readonly IEmailSender _emailSender;
        
        public AuthenticationController(UserManager<User> userManager
            , AuthUserCrudHandler authUser
            , IEmailSender emailSender)
        {
            _userManager        = userManager;
            _authUser           = authUser;
            _emailSender        = emailSender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto user)
        {
            var _user = _authUser.Register(user)
                .Match(Left: e => null, Right: c => c);

            var result = await _userManager.CreateAsync(FromDto(_user), user.Password);
            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(e => $"{e.Description}");
                return BadRequest(new RegistrationResponseDto() { Errors = errors});
            }

            var savedUser = await _userManager.FindByEmailAsync(_user.Email);
            if (!user.Roles.Any())
            {
                await _userManager.AddToRoleAsync(savedUser, "Manager");
            }
            else
            {
                await _userManager.AddToRolesAsync(savedUser, _user.Roles);
            }
            
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(savedUser);

            var confirmationLink = Url.Action(""
               , "confirmemail"
               , new { token = "mtoken" }
               , Request.Scheme);

           
            var bytesEmail = Encoding.UTF8.GetBytes(user.Email);
            var encodeEmail = Convert.ToBase64String(bytesEmail);

            confirmationLink = confirmationLink.Replace("%2F", "/");
            confirmationLink = confirmationLink.Replace("?token", "/");
            confirmationLink = confirmationLink.Replace("mtoken", token);
            confirmationLink = $"{confirmationLink}/{encodeEmail}";
            confirmationLink = confirmationLink.Replace("http://localhost:5000/", "http://localhost:5001/");

            var message = new Message(new string[] { user.Email }, "Confirmar Email"
                , confirmationLink
                , null);

            System.Diagnostics.Debug.WriteLine($"message {message}");
            await _emailSender.SendEmailAsync(message, EmailTemplateType.Register);

            return StatusCode(201);
        }

        [HttpGet("confirmemail", Name ="confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var decodedTokenBytes = Convert.FromBase64String(token);
            var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

            var decodedEmailBytes = Convert.FromBase64String(email);
            var decodedEmail = Encoding.UTF8.GetString(decodedEmailBytes);

            var user = await _userManager.FindByEmailAsync(decodedEmail);
            if (user is null) return NotFound("Confirmation email not found");

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            return result.Succeeded 
                ? Ok("email confirmed") 
                : BadRequest(string.Join(" ",result.Errors.Select(c => $"{c.Code} - {c.Description}")));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(UserLoginDto user)
        {
            if(!await _authUser.ValidateUser(user)) return Unauthorized();

            return Ok(new { Token = await _authUser.CreateToken()});
        }

        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(UserForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if(user is null) return BadRequest("user not valid");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callback = Url.Action(nameof(ResetPassword)
                , "api/authentication"
                , new { token = "mtoken", email = user.Email }
                , Request.Scheme);
            callback = callback.Replace("%2F","/");

            var bytesToken = Encoding.UTF8.GetBytes(token);
            var encodeToken = Convert.ToBase64String(bytesToken);
            callback = callback.Replace("mtoken", encodeToken);

            var message = new Message(new [] { user.Email}, "restablezca su contraseña", callback, null);
            await _emailSender.SendEmailAsync(message, EmailTemplateType.ForgotPassword);
            
            return Ok();
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromQuery] string token, [FromQuery] string email
            , [FromBody] UserResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user is null) return BadRequest("Invalid user");

            var decodedBytes = Convert.FromBase64String(token);
            var decodedToken = Encoding.UTF8.GetString(decodedBytes);

            var resetResult = await _userManager.ResetPasswordAsync(user, decodedToken, dto.Password);
            if(!resetResult.Succeeded)
            {
                var errors = string.Join(" ", resetResult.Errors.Select(c => $"{c.Code} - {c.Description}"));
                return BadRequest(errors);
            }
            return Ok();
        }

    }
}