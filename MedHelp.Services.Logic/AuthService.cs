using MedHelp.Access;
using MedHelp.Services.Model;
using MedHelp.Services.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MedHelp.Services.Logic
{
    public class AuthService : IAuthService
    {
        private readonly IAuthAccess _authAccess;
        private readonly int _liveTimeAccessTokenMinutes;
        private readonly int _liveTimeRefreshTokenHours;
        private readonly string _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthUsing"/> class.
        /// </summary>
        /// <param name="access">The access.</param>
        /// <param name="config">The configuration.</param>
        public AuthService(IAuthAccess access, IConfiguration config)
        {
            _authAccess = access;
            _key = config.GetSection("SecreteKey").Value;
            _liveTimeAccessTokenMinutes = int.Parse(config.GetSection("LiveTimeAccessTokenMinutes").Value);
            _liveTimeRefreshTokenHours = int.Parse(config.GetSection("LiveTimeRefreshTokenHours").Value);
        }

        /// <inheritdoc/>
        public async Task<Tokens> LoginAsync(User user)
        {
            var userData = await _authAccess.GetUserAsync(user.Login);

            if (userData is null || userData.Password != user.Password)
                return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userData.Login)
            };

            var accessToken = GenerateAccessToken(claims);
            var refreshToken = GenerateRefreshToken();

            userData.RefreshToken = refreshToken;
            userData.RefreshTokenExpiryTime = DateTime.Now.AddHours(_liveTimeRefreshTokenHours);

            await _authAccess.SetNewRefreshKeyAsync(userData);

            return new Tokens()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        /// <inheritdoc/>
        public async Task<int> RegistrationAsync(User user)
        {
            var userDb = MapUserToDb(user);

            return await _authAccess.AddUserAsync(userDb);
        }

        /// <inheritdoc/>
        public async Task<List<User>> GetUsersAsync()
        {
            var users = await _authAccess.GetUsersAsync();

            var userModels = users.Select(us => MapUserToMod(us)).ToList();

            return userModels;
        }

        /// <inheritdoc/>
        public async Task<int> UpdateUserAsync(User user)
        {
            var userModel = MapUserToDb(user);

            return await _authAccess.UpdateUserAsync(userModel);
        }

        /// <inheritdoc/>
        public async Task<int> DeleteUserAsync(int id)
        {
            return await _authAccess.DeleteUserAsync(id);
        }

        public async Task<User> GetUser(string login)
        {
            var user = await _authAccess.GetUserAsync(login);

            if (user is null)
                return null;

            var userModel = MapUserToMod(user);

            return userModel;
        }

        public async Task<List<Sex>> GetAllSexes()
        {
            return (await _authAccess.GetAllSexes()).Select(sex => new Sex() { Value = sex.Value }).ToList();
        }

        public async Task<int> AddUserAsync(User user)
        {
            var userDb = MapUserToDb(user);

            return await _authAccess.AddUserAsync(userDb);
        }

        public async Task<List<User>> SearchUsersAsync(string searchString)
        {
            var users = await _authAccess.SearchUserAsync(searchString);

            var userModels = users.Select(us => MapUserToMod(us)).ToList();

            return userModels;
        }

        private string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: claims,
                expires: DateTime.Now.AddMinutes(_liveTimeAccessTokenMinutes),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        private Access.Entity.User MapUserToDb(User user)
        {
            var userDb = new Access.Entity.User()
            {
                Login = user.Login,
                Password = user.Password,
            };

            if (user.Doctor is not null)
            {
                var doctor = new Access.Entity.Doctor()
                {
                    Name = user.Doctor.Name,
                    FirstName = user.Doctor.FirstName,
                    LastName = user.Doctor.LastName,
                    NumberOfPhone = user.Doctor.NumberOfPhone,
                    Specialization = user.Doctor.Specialization,
                };

                userDb.Doctor = doctor;
            }
            else
            {
                var sex = new Access.Entity.Sex()
                {
                    Value = user.Patient.Sex.Value
                };

                var patient = new Access.Entity.Patient()
                {
                    Name = user.Patient.Name,
                    FirstName = user.Patient.FirstName,
                    LastName = user.Patient.LastName,
                    NumberOfPhone = user.Patient.NumberOfPhone,
                    DateOfDirth = user.Patient.DateOfDirth,
                    Sex = sex
                };
                
                userDb.Patient = patient;
            }

            return userDb;
        }

        private User MapUserToMod(Access.Entity.User user)
        {
            var userMod = new User()
            {
                UserId = user.UserId,
                Login = user.Login,
                Password = user.Password,
            };

            if (user.Doctor is not null)
            {
                var doctor = new Doctor()
                {
                    DoctorId = user.Doctor.DoctorId,
                    Name = user.Doctor.Name,
                    FirstName = user.Doctor.FirstName,
                    LastName = user.Doctor.LastName,
                    NumberOfPhone = user.Doctor.NumberOfPhone,
                    Specialization = user.Doctor.Specialization,
                };

                userMod.Doctor = doctor;
            }
            else
            {
                var sex = new Sex()
                {
                    SexId = user.Patient.Sex.SexId,
                    Value = user.Patient.Sex.Value
                };

                var patient = new Patient()
                {
                    PatientId = user.Patient.PatientId,
                    Name = user.Patient.Name,
                    FirstName = user.Patient.FirstName,
                    LastName = user.Patient.LastName,
                    NumberOfPhone = user.Patient.NumberOfPhone,
                    DateOfDirth = user.Patient.DateOfDirth,
                    Sex = sex
                };

                userMod.Patient = patient;
            }

            return userMod;
        }
    }
}
