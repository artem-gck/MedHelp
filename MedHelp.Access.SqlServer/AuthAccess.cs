using MedHelp.Access.Context;
using MedHelp.Access.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MedHelp.Access.SqlServer
{
    public class AuthAccess : IAuthAccess
    {
        private readonly MedHelpContext _medHelpContext;

        public AuthAccess(MedHelpContext medHelpContext)
        {
            _medHelpContext = medHelpContext;
        }

        public async Task<User> GetUserAsync(string login)
        {
            login = login is not null ? login : throw new ArgumentNullException(nameof(login));

            return await _medHelpContext.Users.Include(us => us.Doctor)
                                              .Include(us => us.Patient)
                                              .ThenInclude(pat => pat.Sex)
                                              .FirstOrDefaultAsync(user => user.Login == login);
        }

        /// <inheritdoc/>
        public async Task<bool> SetNewRefreshKeyAsync(User user)
        {
            user = user is not null ? user : throw new ArgumentNullException(nameof(user));

            var userData = await _medHelpContext.Users.FirstOrDefaultAsync(userData => userData.Login == user.Login);

            userData.RefreshToken = user.RefreshToken;
            userData.RefreshTokenExpiryTime = user.RefreshTokenExpiryTime;

            await _medHelpContext.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc/>
        public async Task<List<User>> GetUsersAsync()
            => await _medHelpContext.Users.Include(user => user.Doctor)
                                          .Include(user => user.Patient)
                                          .ThenInclude(pat => pat.Sex)
                                          .ToListAsync();

        /// <inheritdoc/>
        public async Task<int> AddUserAsync(User user)
        {
            var userE = await _medHelpContext.Users.AddAsync(user);
            await _medHelpContext.SaveChangesAsync();

            return userE.Entity.UserId;
        }

        /// <inheritdoc/>
        public async Task<int> UpdateUserAsync(User user)
        {
            var us = await _medHelpContext.Users.Include(user => user.Doctor)
                                                .Include(user => user.Patient)
                                                .ThenInclude(pat => pat.Sex)
                                                .FirstOrDefaultAsync(us => us.Login == user.Login);

            us.Login = user.Login;
            us.Password = user.Password;

            if (us.Doctor is not null)
            {
                us.Doctor.Name = user.Doctor.Name;
                us.Doctor.FirstName = user.Doctor.FirstName;
                us.Doctor.LastName = user.Doctor.LastName;
                us.Doctor.Specialization = user.Doctor.Specialization;
                us.Doctor.NumberOfPhone = user.Doctor.NumberOfPhone;
            }
            else
            {
                var sex = GetSex(user.Patient.Sex.Value);

                us.Patient.Name = user.Patient.Name;
                us.Patient.FirstName = user.Patient.FirstName;
                us.Patient.LastName = user.Patient.LastName;
                us.Patient.DateOfDirth = user.Patient.DateOfDirth;
                us.Patient.NumberOfPhone = user.Patient.NumberOfPhone;
            }

            await _medHelpContext.SaveChangesAsync();

            return us.UserId;
        }

        /// <inheritdoc/>
        public async Task<int> DeleteUserAsync(int id)
        {
            var us = await _medHelpContext.Users.FirstOrDefaultAsync(us => us.UserId == id);

            _medHelpContext.Users.Remove(us);
            await _medHelpContext.SaveChangesAsync();

            return us.UserId;
        }

        public async Task<List<Sex>> GetAllSexes()
            => await _medHelpContext.Sexes.ToListAsync();

        public async Task<List<User>> SearchUserAsync(string searchString)
            => await _medHelpContext.Users.Include(user => user.Doctor)
                                          .Include(user => user.Patient)
                                          .ThenInclude(pat => pat.Sex)
                                          .Where(us => us.Login.Contains(searchString) ||
                                                       us.Doctor.Name.Contains(searchString) ||
                                                       us.Doctor.FirstName.Contains(searchString) ||
                                                       us.Doctor.LastName.Contains(searchString) ||
                                                       us.Doctor.Specialization.Contains(searchString) ||
                                                       us.Doctor.NumberOfPhone.Contains(searchString) ||
                                                       us.Patient.Name.Contains(searchString) ||
                                                       us.Patient.FirstName.Contains(searchString) ||
                                                       us.Patient.LastName.Contains(searchString) ||
                                                       us.Patient.DateOfDirth.ToString().Contains(searchString) ||
                                                       us.Patient.NumberOfPhone.Contains(searchString))
                                          .ToListAsync();

        private async Task<Sex> GetSex(string sex)
        {
            var sexDb = await _medHelpContext.Sexes.FirstOrDefaultAsync(r => r.Value == sex);

            if (sexDb is null)
            {
                var newSex = new Sex()
                {
                    Value = sex
                };

                var se = await _medHelpContext.Sexes.AddAsync(newSex);

                return se.Entity;
            }

            return sexDb;
        }
    }
}
