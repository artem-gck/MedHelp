using MedHelp.Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHelp.Services
{
    public interface ITokenService
    {
        public Task<Tokens> Refresh(Tokens tokenApiModel);
        public Task<bool> CheckAccessKey(string token);
    }
}
