using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    public IEnumerable<VaultKeep> GetAll()
    {
      return _repo.GetAll();
    }

    public VaultKeep GetOne(int id)
    {
      return _repo.GetOne(id);
    }

    public VaultKeep Create(VaultKeep newVaultKeep)
    {
      return _repo.Create(newVaultKeep);
    }

    public bool Delete(int id, string userId)
    {
      return _repo.Delete(id, userId);
    }
  }
}