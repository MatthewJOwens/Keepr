using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Vault> GetByUser(string userId)
    {
      return _repo.GetByUser(userId);
    }

    internal Vault Create(Vault newVault)
    {
      if (newVault.Description == null)
      {
        newVault.Description = "No description given.";
      }
      return _repo.Create(newVault);
    }

    internal string Delete(int id, string userId)
    {
      Vault foundVault = GetById(id);
      if (foundVault == null)
      {
        throw new Exception("Unknown vault ID");
      }
      if (foundVault.UserId != userId)
      {
        throw new Exception("Not your vault!");
      }
      if (_repo.Delete(id, userId))
      {
        return "Successfully deleted vault.";
      }
      throw new Exception("Failed to delete vault.");
    }

    internal Vault GetById(int id)
    {
      return _repo.GetById(id);
    }
  }
}