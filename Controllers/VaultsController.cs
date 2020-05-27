using System;
using System.Collections.Generic;
using System.Security.Claims;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly KeepsService _ks;
    public VaultsController(VaultsService vs, KeepsService ks)
    {
      _vs = vs;
      _ks = ks;
    }

    [HttpGet]
    [Authorize]
    public ActionResult<IEnumerable<Vault>> GetByUser()
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to get your vaults.");
        }
        string userId = user.Value;
        return Ok(_vs.GetByUser(userId));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public ActionResult<Vault> Create([FromBody] Vault newVault)
    {
      try
      {
        Claim userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
          throw new Exception("You must be logged in to create a vault.");
        }
        newVault.UserId = userId.Value;
        return Ok(_vs.Create(newVault));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    [Authorize]
    public ActionResult<IEnumerable<Keep>> GetKeepsByVaultId(int id)
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to get vaulted keeps.");
        }
        string userId = user.Value;
        return Ok(_ks.GetKeepsByVaultId(id, userId));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<string> Delete(int id)
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to delete a vault.");
        }
        string userId = user.Value;
        return Ok(_ks.Delete(id, userId));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}