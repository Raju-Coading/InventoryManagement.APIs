using InventoryManagement.Application.Interfaces;
using System.Collections.Concurrent;

namespace InventoryManagement.Infrastructure.Security;

public class TokenRevocationService : ITokenRevocationService
{
    private readonly ConcurrentDictionary<string, bool> _revokedTokens = new();

    public void RevokeToken(string token)
    {
        _revokedTokens.TryAdd(token, true);
    }

    public bool IsTokenRevoked(string token)
    {
        return _revokedTokens.ContainsKey(token);
    }
}