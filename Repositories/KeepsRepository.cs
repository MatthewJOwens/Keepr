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

    internal Keep Create(Keep keepData)
    {
      keepData.views = 0;
      keepData.shares = 0;
      keepData.keeps = 0;
      string sql = @"INSERT INTO keeps
        (name, description, userId, img, isPrivate, views, shares, keeps)
        VALUES
        (@Name, @Description, @UserId, @Img, @IsPrivate, @Views, @Shares, @Keeps);
        SELECT LAST_INSERT_ID()";
      keepData.Id = _db.ExecuteScalar<int>(sql, keepData);
      return keepData;
    }
  }
}