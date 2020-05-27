using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Keep> Get()
    {
      string sql = "SELECT * FROM keeps WHERE isPrivate = 0;";
      return _db.Query<Keep>(sql);
    }
    internal Keep GetById(int id)
    {
      string sql = "SELECT * FROM keeps WHERE id = @Id LIMIT 1";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    }

    internal IEnumerable<Keep> GetByUser(string userId)
    {
      string sql = "SELECT * FROM keeps WHERE userId = @UserId";
      return _db.Query<Keep>(sql, new { userId });
    }

    internal Keep Create(Keep keepData)
    {
      keepData.Views = 0;
      keepData.Shares = 0;
      keepData.Keeps = 0;
      string sql = @"INSERT INTO keeps
        (name, description, userId, img, isPrivate, views, shares, keeps)
        VALUES
        (@Name, @Description, @UserId, @Img, @IsPrivate, @Views, @Shares, @Keeps);
        SELECT LAST_INSERT_ID()";
      keepData.Id = _db.ExecuteScalar<int>(sql, keepData);
      return keepData;
    }

    internal bool UpdateViewCount(Keep keepToUpdate)
    {
      string sql = @"
      UPDATE keeps
      SET views = views + 1
      WHERE id = @Id";
      int affectedRows = _db.Execute(sql, keepToUpdate);
      return affectedRows == 1;
    }
    internal bool UpdateKeptCount(Keep keepToUpdate)
    {
      string sql = @"
      UPDATE keeps
      SET keeps = keeps + 1
      WHERE id = @Id";
      int affectedRows = _db.Execute(sql, keepToUpdate);
      return affectedRows == 1;
    }

    internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId, string userId)
    {
      string sql = @"
        SELECT 
        k.*,
        vk.id as vaultKeepId
        FROM vaultkeeps vk
        INNER JOIN keeps k ON k.id = vk.keepId 
        WHERE (vaultId = @VaultId AND vk.userId = @UserId)";
      return _db.Query<Keep>(sql, new { vaultId, userId });
    }

    internal bool Delete(int id, string userId)
    {
      string sql = "DELETE FROM keeps WHERE id = @id AND userId = @userId";
      int affectedRows = _db.Execute(sql, new { id, userId });
      return affectedRows == 1;
    }
  }
}