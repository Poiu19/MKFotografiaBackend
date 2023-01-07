using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MKFotografiaBackend.Entities;
using MKFotografiaBackend.Exceptions;
using MKFotografiaBackend.Models;
using MKFotografiaBackend.Models.Incoming;
using MKFotografiaBackend.Models.Outgoing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MKFotografiaBackend.Services
{
    public interface IUserService
    {
        int RegisterUser(RegisterUserDto dto);
        string Login(LoginDto dto);
        UserDto GetUser(int userId);
        List<UserDto> GetUsers();
        RoleDto GetRole(int userId);
        void SetRole(int userId, SetRoleDto dto);
        void SetActive(int userId, bool active);
        void ChangeUser(int userId, ChangeUserDto dto);
        void ResetAdmin(ResetAdminDto dto);
    }
    public class UserService : IUserService
    {
        private readonly MKDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserService(MKDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }
        public void ChangeUser(int userId, ChangeUserDto dto)
        {
            throw new NotImplementedException();
        }

        public RoleDto GetRole(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDto GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        public List<UserDto> GetUsers()
        {
            throw new NotImplementedException();
        }

        public string Login(LoginDto dto)
        {
            var userEntity = _dbContext
                .Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);
            if (userEntity is null)
            {
                throw new BadRequestException("Błędny login lub hasło.");
            }

            var verification = _passwordHasher.VerifyHashedPassword(userEntity, userEntity.PasswordHash, dto.Password);
            if (verification == PasswordVerificationResult.Failed) {
                throw new BadRequestException("Błędny login lub hasło.");
            }
            if (!userEntity.Active)
            {
                throw new ForbiddenException();
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userEntity.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{userEntity.Name} {userEntity.LastName}"),
                new Claim(ClaimTypes.Role, $"{userEntity.Role.Name}"),
                new Claim("RoleId", userEntity.RoleId.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expires, signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            userEntity.LastLogin = DateTime.Now;
            _dbContext.SaveChanges();
            return tokenHandler.WriteToken(token);
        }

        public int RegisterUser(RegisterUserDto dto)
        {
            var userEntity = new User()
            {
                Email = dto.Email,
                RoleId = dto.RoleId,
                Name = dto.Name,
                LastName = dto.LastName
            };
            var password = _passwordHasher.HashPassword(userEntity, dto.Password);
            userEntity.PasswordHash = password;
            _dbContext.Users.Add(userEntity);
            _dbContext.SaveChanges();
            return userEntity.Id;
        }

        public void ResetAdmin(ResetAdminDto dto)
        {
            var admin = _dbContext.Users.FirstOrDefault(u => u.Id == 1);
            if (admin is null)
            {
                throw new NotFoundException("Konto admina nie istnieje.");
            }
            var password = _passwordHasher.HashPassword(admin, dto.Password);
            admin.PasswordHash = password;
            admin.Email = "admin@admin.pl";
            admin.Name = "Admin";
            admin.LastName = "Admin";
            admin.RoleId = 4;
            admin.Active = true;
            _dbContext.SaveChanges();

        }

        public void SetActive(int userId, bool active)
        {
            throw new NotImplementedException();
        }

        public void SetRole(int userId, SetRoleDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
