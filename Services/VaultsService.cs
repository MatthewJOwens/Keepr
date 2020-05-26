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
  }
}