using MedHelp.Client.Models;

namespace MedHelp.Client.Services
{
    public interface IBasicService
    {
        public Task<List<Sex>> GetAllSexes();
    }
}
