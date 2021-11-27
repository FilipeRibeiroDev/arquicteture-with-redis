using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arquicteture.API.Model
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string username, string password);
        Task<User> GetUser(string username);
        Task<User> Create(User user);
        Task<User> Update(User user);
        Task<bool> Delete(string username);
    }
}
