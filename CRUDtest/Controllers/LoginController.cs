using CRUDtest.Person;
using FirstProject.Persons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace CRUDtest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        
        private IConfiguration _configuration;
        private IDataProtector _protector;

        
        public LoginController(IConfiguration config, IDataProtectionProvider provider)
        {
            _configuration = config;
            _protector = provider.CreateProtector(GetType().FullName);
        }

        private string Generate(User foo)
        {
            var stringKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(stringKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, foo.Role),
                new Claim(ClaimTypes.NameIdentifier, foo.Username)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                                             _configuration["Jwt:Audience"],
                                             claims,
                                             expires:DateTime.Now.AddMinutes(15),
                                             signingCredentials : credentials
                                            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //[HttpPost]
        private User Authenticate(BoundedReq boundedReq)
        {
            var currentUser = UserConstants.notCapableOfDoingDatabases(_protector, boundedReq).FirstOrDefault(o => o.Username.ToLower() == boundedReq.UserLogging.Username && o.Password == boundedReq.UserLogging.Password);
            
            if (currentUser != null) 
            {
                return currentUser;
            }

            return null;
        }

        
        [AllowAnonymous]
        [HttpPost(Name = "LoginAPI")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        /// <response code="404"> when the user is not found </response>
        /// <response code="200"> when the login has been successful </response>

        public IActionResult Login(BoundedReq boundedReq) 
        {
            var user = Authenticate(boundedReq);

            if (user != null) 
            {
                var token = Generate(user);
                return new ObjectResult(token) { StatusCode = (int)HttpStatusCode.OK };
            }

            return new ObjectResult("User Not Found") { StatusCode = (int)HttpStatusCode.NotFound };
           
        
        }
        

    }
}
