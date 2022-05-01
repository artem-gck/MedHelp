using MedHelp.Services.Model;
using MedHelp.Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Services
{
    public interface IAuthService
    {
        public Task<Tokens> LoginAsync(User user);
        public Task<int> RegistrationAsync(User user);
        public Task<List<User>> GetUsersAsync();
        public Task<int> UpdateUserAsync(User user);
        public Task<int> DeleteUserAsync(int id);
        public Task<User> GetUser(string login);
        public Task<List<Sex>> GetAllSexes();
        public Task<int> AddUserAsync(User user);
        public Task<List<User>> SearchUsersAsync(string searchString);
    }
}
