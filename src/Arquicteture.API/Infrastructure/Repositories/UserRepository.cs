using Arquicteture.API.Model;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Arquicteture.API.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;

        private readonly IDistributedCache _redis;

        public UserRepository(ILogger<UserRepository> logger, IDistributedCache redis)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _redis = redis ?? throw new ArgumentNullException(nameof(redis));
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var data = await GetUser(username);

            if (!data.Password.Equals(password))
            {
                return null;
            }

            return data;
        }

        public async Task<User> Create(User user)
        {
            await _redis.SetStringAsync(user.Username, JsonSerializer.Serialize(user));

            _logger.LogInformation("User item persisted succesfully.");

            return await GetUser(user.Username);
        }

        public async Task<bool> Delete(string username)
        {
            await _redis.RemoveAsync(username);
            return true;
        }

        public async Task<User> Update(User user)
        {
            await _redis.SetStringAsync(user.Username, JsonSerializer.Serialize(user));

            _logger.LogInformation("User item persisted succesfully.");

            return await GetUser(user.Username);
        }

        public async Task<User> GetUser(string username)
        {
           
            var data = await _redis.GetStringAsync(username);

            if (string.IsNullOrEmpty(data))
            {
                return null;
            }


            var user = JsonSerializer.Deserialize<User>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            });
            return user;

        }
    }
}
