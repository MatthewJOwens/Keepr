using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Keep> Get()
    {
      return _repo.Get();
    }
    public Keep GetById(int id)
    {
      return _repo.GetById(id);
    }
    public IEnumerable<Keep> GetByUser(string userId)
    {
      return _repo.GetByUser(userId);
    }
    public Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }

    public string Delete(int id, string userId)
    {
      Keep foundKeep = GetById(id);
      if (foundKeep.UserId != userId)
      {
        throw new Exception("This is not yours!");
      }
      if (_repo.Delete(id, userId))
      {
        return "Successfully deleted.";
      }
      throw new Exception("Failed to delete");
    }

    public Keep Edit(Keep keepToUpdate)
    {
      Keep foundKeep = GetById(keepToUpdate.Id);
      // route for Views
      if (foundKeep != null)
      {
        if (foundKeep.Views < keepToUpdate.Views)
        {

          if (_repo.UpdateViewCount(foundKeep))
          {
            return foundKeep;
          }
          return foundKeep;
        }
        // route for Kept
        if (foundKeep.Keeps < keepToUpdate.Keeps)
        {

          if (_repo.UpdateKeptCount(foundKeep))
          {
            return foundKeep;
          }
          return foundKeep;
        }
        // route for Shares
      }
      throw new Exception("Unable to edit.");
    }

    public IEnumerable<Keep> GetKeepsByVaultId(int vaultId, string userId)
    {
      return _repo.GetKeepsByVaultId(vaultId, userId);
    }
  }
}