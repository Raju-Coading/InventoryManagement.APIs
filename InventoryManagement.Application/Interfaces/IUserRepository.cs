using System;
using System.Collections.Generic;
using System.Text;

using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    Task AddAsync(User user);

    Task<bool> EmailExistsAsync(string email);
}