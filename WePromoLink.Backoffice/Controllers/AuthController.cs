using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GoogleAuthenticatorService.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WePromoLink.DTO.Auth;

namespace WePromoLink.Backoffice.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Login login)
    {
        string user = _configuration["Auth:user"];
        string password = _configuration["Auth:password"];
        if (user != login.User || password != login.Password) return new StatusCodeResult(401);

        TwoFactorAuthenticator Authenticator = new TwoFactorAuthenticator();
        if (!Authenticator.ValidateTwoFactorPIN(_configuration["Auth:secret"], login.Token)) return new StatusCodeResult(401); ;

        // Si la autenticación es exitosa, genera un token JWT y lo devuelve al cliente.
        var token = GenerateJwtToken(login.User);
        return new OkObjectResult(token);
    }

    private string GenerateJwtToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Auth:secret"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = "wepromolink.com",
            Issuer = "wepromolink.com",
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = DateTime.UtcNow.AddDays(3), // Tiempo de expiración del token
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
