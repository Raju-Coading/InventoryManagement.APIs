using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(int userId, string email);
}
