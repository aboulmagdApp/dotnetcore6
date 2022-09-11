using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Data.Auth
{
    public interface IAuthRepository
    {
        Task<ServiceRespones<int>> Register(User user, string password);
        Task<ServiceRespones<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}