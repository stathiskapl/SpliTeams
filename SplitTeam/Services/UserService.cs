using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SplitTeam.Model;
using SplitTeam.ModelDtos;
using SplitTeam.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SplitTeam.Services
{
    public interface IUserService
    {
        Task<User> AddUser(UserToLoginDto user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(User user);
        Task<User> GetUserById(int userId);
        string Authenticate(User user);
        Task<User> Login(string username, string password);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, 
            IConfiguration configuration, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<User> AddUser(UserToLoginDto user)
        {
            if (!(await _userRepository.UserExists(user.Username)))
            {
                var role = await _roleRepository.GetDefaultRole();
                var userTosave = _mapper.Map<User>(user);
                userTosave.Role = role;
                return await _userRepository.AddUser(userTosave);
            }

            return null;
        }

        public string Authenticate(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName.ToString())
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("Appsettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<User> DeleteUser(User user)
        {
            return await _userRepository.DeleteUser(user);
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _userRepository.GetUserById(userId);
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _userRepository.GetUserFromUsernameAndPassword(username, password);
            if (user == null)
                return null;
            var token = Authenticate(user);
            if (token != null)
            {
                user.Token = token;
            }
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            user.Role = await _roleRepository.GetRoleById(user.Role.Id);
            return await _userRepository.UpdateUser(user);
        }
    }
}
