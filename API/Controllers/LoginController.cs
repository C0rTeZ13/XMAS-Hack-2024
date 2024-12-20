using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Models;
using ServiceLayer.Models.Exceptions;
using ServiceLayer.Services.Authentication;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public LoginController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult<TokenDto>> Login(AuthRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await _tokenService.CreateTokenAsync(request.UserName, request.Password, cancellationToken);
            }
            catch (ClientSideException e)
            {
                return new ObjectResult(e.Message) { StatusCode = 400 };
            }
        }
    }
}
