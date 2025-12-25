using System.Collections.Concurrent;
using System.Security.Cryptography;


namespace School.Services
{
    public class LoginTokenEntry
    {
        public int UserId { get; set; }
        public DateTime ExpireAt { get; set; }
    }

    public class LoginTokenService
    {
        private readonly ConcurrentDictionary<string, LoginTokenEntry> _tokens = new();

        public string CreateToken(int userId, TimeSpan? ttl = null)
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(32);
            string token = Convert.ToBase64String(tokenBytes)
                .Replace('+', '-').Replace('/', '_').TrimEnd('=');

            var entry = new LoginTokenEntry()
            {
                UserId = userId,
                ExpireAt = DateTime.UtcNow + (ttl ?? TimeSpan.FromMinutes(5))
            };

            _tokens[token] = entry;
            return token;
        }

        public int? ValidateAndConsume(string token) 
        {
            if (string.IsNullOrEmpty(token)) return null;

            if (!_tokens.TryGetValue(token, out var entry)) return null;

            if (entry.ExpireAt <= DateTime.UtcNow)
            {
                _tokens.TryRemove(token, out _);
                return null;
            }

            // Consume
            _tokens.TryRemove(token, out _);
            return entry.UserId;
        }

        public void PurgeExpired()
        {
            var now = DateTime.UtcNow;
            foreach (var kv in _tokens)
            {
                if (kv.Value.ExpireAt < now)
                    _tokens.TryRemove(kv.Key, out _);
            }
        }

    }
}
