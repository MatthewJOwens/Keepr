using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;
    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      try
      {
        return Ok(_ks.Get());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

    [HttpGet("user")]
    [Authorize]
    public ActionResult<IEnumerable<Keep>> GetByUser()
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to access.");
        }
        string userId = user.Value;
        return Ok(_ks.GetByUser(userId));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public ActionResult<Keep> Post([FromBody] Keep newKeep)
    {
      try
      {
        Claim userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
          throw new Exception("You must be logged in to access.");
        }
        newKeep.UserId = userId.Value;
        return Ok(_ks.Create(newKeep));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
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
          throw new Exception("You must be logged in to delete.");
        }
        string userId = user.Value;
        return Ok(_ks.Delete(id, userId));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPut("{id}")]
    public ActionResult<Keep> Edit(int id, [FromBody] Keep keepToUpdate)
    {
      keepToUpdate.Id = id;
      try
      {
        return Ok(_ks.Edit(keepToUpdate));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}