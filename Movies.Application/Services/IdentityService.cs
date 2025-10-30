using Microsoft.IdentityModel.Tokens;
using Movies.Application.Models;
using Movies.Application.Options;
using Movies.Application.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly JwtSettings _jwtSettings;
        public IdentityService(IIdentityRepository identityRepository, JwtSettings jwtSettings)
        {
            _identityRepository = identityRepository;
            _jwtSettings = jwtSettings;
        }
        public async Task<AuthenticationResult> LoginAsync(User user, CancellationToken token = default)
        {
            var result = await _identityRepository.ExistsByEmailAsync(user.Email, token);
            if (!result)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = new[] { "User with this email does not exist" },
                    success = false
                };
            }


            var invocedUser = await _identityRepository.GetUserByEmail(user.Email, token);

            var checkPassword = await CheckPassword(user,invocedUser, token);
            if (!checkPassword)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = new[] { "password does not match our record" },
                    success = false
                };
            }

            return new AuthenticationResult
            {
                success = true,
                token = await CreateToken(invocedUser, token)
            };
        }

        public async Task<AuthenticationResult> RegisterAsync(User user, CancellationToken token = default)
        {

            var result = await _identityRepository.ExistsByEmailAsync(user.Email, token);
            if (result)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = new[] { "User with this email already exists" },
                    success = false
                };
            }
            var createResult = await _identityRepository.CreateAsync(user, token);

            if (!createResult)
            {
                return new AuthenticationResult
                {
                    ErrorMessages = new[] { "Error while creating user" },
                    success = false
                };
            }

            return new AuthenticationResult
            {
                success = true,
                token = await CreateToken(user, token)
            };

        }

        public async Task<string> CreateToken(User user, CancellationToken token = default)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("admin", user.isAdmin.ToString().ToLower()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
                ), Issuer = _jwtSettings.Issuer, 
                Audience = _jwtSettings.Audience
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }


        public Task<bool> CheckPassword(User user, User invoced, CancellationToken token = default)
        {
            if(user.Password != invoced.Password)
            {
                return Task.FromResult(false);
            }
            else
            {
                return Task.FromResult(true);
            }
        }

    }
    }
