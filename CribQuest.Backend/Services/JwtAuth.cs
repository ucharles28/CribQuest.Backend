using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CribQuest.Backend.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CribQuest.Backend.Services;

public class JwtAuth : IJwtAuth
{
    private readonly AppSettings _appSettings;

    public JwtAuth(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }
    public string Authentication( User user = null)
    {
        // 1. Create Security Token Handler
        var tokenHandler = new JwtSecurityTokenHandler();

        // 2. Create Private Key to Encrypted
        var tokenKey = Encoding.ASCII.GetBytes("THIS IS USED TninjknO SIGN AND VERIFY JWT TOKENS, REPLAChcyuiE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING");


        //3. Create JETdescriptor
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Expires = DateTime.UtcNow.AddMonths(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        if (user != null)
        {
            tokenDescriptor.Subject = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    // new Claim(ClaimTypes.Role, Enum.GetName(typeof(AccountType), user.AccountType))
                });
        }
        //4. Create Token
        var token = tokenHandler.CreateToken(tokenDescriptor);
        //Set Expiry
        // 5. Return Token from method
        return tokenHandler.WriteToken(token);
    }
}