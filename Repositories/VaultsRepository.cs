using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Vault> GetByUser(string userId)
    {
      string sql = "SELECT * FROM vaults WHERE userId = @UserId";
      return _db.Query<Vault>(sql, new { userId });
    }

    internal Vault Create(Vault newVault)
    {
      string sql = @"INSERT INTO vaults
        (name, description, userId)
        VALUES
        (@Name, @Description, @UserId);
        SELECT LAST_INSERT_ID()";
      newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
      return newVault;
    }
  }
}