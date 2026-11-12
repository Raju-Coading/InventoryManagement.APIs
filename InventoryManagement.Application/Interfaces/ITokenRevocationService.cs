using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Application.Interfaces;

public interface ITokenRevocationService
{
    void RevokeToken(string token);

    bool IsTokenRevoked(string token);


}