using Base.Server.API.Models;
using Base.Server.API.Models.Entities;
using Base.Server.API.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Models.Requests;
using Shared.Models.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Base.Server.API.Services;

public class JwtAuthenticationService : IJwtAuthenticationService
{
    private readonly JSONWebTokensSettings _settings;
    private readonly IUserRepository _userRepository;

    public JwtAuthenticationService(IOptions<JSONWebTokensSettings> settings, IUserRepository userRepository)
    {
        _settings = settings.Value;
        _userRepository = userRepository;
    }

    public async Task<UserAuthenticationResponse?> Authenticate(UserAuthenticationRequest userAuthenticationRequest)
    {
        User? user = await _userRepository.GetByUsername(userAuthenticationRequest.Username);
        if (user is null)
        {
            return null;
        }

        if (!BCrypt.Net.BCrypt.Verify(userAuthenticationRequest.Password, user.Password))
            throw new Exception("Username or password is incorrect");

        if (!user.IsEnabled)
            throw new UnauthorizedAccessException("Your account is blocked.");

        JwtSecurityToken jwtSecurityToken = GenerateToken(user);
        return new UserAuthenticationResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
        };
    }

    private JwtSecurityToken GenerateToken(User user)
    {
        List<Claim> roleClaims = new List<Claim>();

        IEnumerable<Claim> claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Name, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id.ToString()),
        }
            .Union(roleClaims);

        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_settings.Key));
        SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }
}