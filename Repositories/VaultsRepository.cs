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
    // TODO write a delete function

    internal bool Delete(int vaultId, string userId)
    {
      string sql = @"DELETE FROM vaults WHERE id = @VaultId AND userId = @UserId LIMIT 1";
      int affectedRows = _db.Execute(sql, new { vaultId, userId });
      return affectedRows == 1;
    }

    internal Vault GetById(int id)
    {
      string sql = "SELECT * FROM keeps WHERE id = @Id LIMIT 1";
      return _db.QueryFirstOrDefault<Vault>(sql, new { id });
    }
  }
}