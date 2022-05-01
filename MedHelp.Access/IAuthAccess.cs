using MedHelp.Access.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Access
{
    public interface IAuthAccess
    {
        public Task<User> GetUserAsync(string login);
        public Task<bool> SetNewRefreshKeyAsync(User user);
        public Task<List<User>> GetUsersAsync();
        public Task<int> AddUserAsync(User user);
        public Task<int> UpdateUserAsync(User user);
        public Task<int> DeleteUserAsync(int id);
        public Task<List<Sex>> GetAllSexes();
        public Task<List<User>> SearchUserAsync(string searchString);
    }
}
