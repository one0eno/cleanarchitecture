using CleanArchitecture.Application.Constants;
using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Identity.Services
{
    public class AuthServices : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;

        public AuthServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IOptions<JWTSettings> jWTSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jWTSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception($"El usuario con email {request.Email} no existe");
            }

            

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if(!result.Succeeded)
                throw new Exception($"Las credenciales son incorrectas");

            var token = await GenerateToken(user);

            var authResponse = new AuthResponse() {
                Email = user.Email, 
                Id = user.Id, 
                Token = new JwtSecurityTokenHandler().WriteToken(token), 
                UserName = user.UserName };

            return authResponse;

        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
           var existingUesr= await _userManager.FindByNameAsync(request.UserName);
            if (existingUesr != null)
            {
                throw new Exception($"El username ya fuen tomado por otra cuenta");
            }
            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail != null)
            {
                throw new Exception($"El email ya está en uso");
            }

            var user = new ApplicationUser()
            {
                Email = request.Email,
                Name = request.Name,
                SurName = request.SurName,
                UserName = request.UserName,
                EmailConfirmed = true,


            };

            var result =  await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
              
                await _userManager.AddToRoleAsync(user, "Operator");

                var token = await GenerateToken(user);
                return new RegistrationResponse
                {
                     Email = user.Email,
                     Token = new JwtSecurityTokenHandler().WriteToken(token),
                     UserId = user.Id,
                     UserName = user.UserName
                };
            }

            throw new Exception($"{result.Errors}");

        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {

            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role,role));
            }

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(CustomClaimsTypes.Uid, user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer:_jwtSettings.Issuer,
                audience:_jwtSettings.Audience,
               claims: claims,
               expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DuarationInMinutes),
               signingCredentials: signingCredentials);

            return jwtSecurityToken;

        }
    }
}
