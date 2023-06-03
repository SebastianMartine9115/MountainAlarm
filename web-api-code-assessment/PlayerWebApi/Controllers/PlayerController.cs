// /////////////////////////////////////////////////////////////////////////////
// YOU CAN FREELY MODIFY THE CODE BELOW IN ORDER TO COMPLETE THE TASK
// /////////////////////////////////////////////////////////////////////////////
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayerWebApi.Data;
using PlayerWebApi.Data.Entities;

namespace PlayerWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly PlayerDbContext _dbContext;

    public PlayerController(PlayerDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /* 
     * What I would do different... Create a business layer (project) where we can enforce data integrityand standards.
     * If we had a clear understanding of what data would look like, we could create data validation nuget packages
     * that an be easily plugged into any other solutions that might need this functionality.
     * 
     * Move reading and writing from the database to a data layer (project). Depending on the application, data reading 
     * and writing packaged can be created and implemented in any other solutions. Sinmply pass a connection string, 
     * the stored procedure name and parameters
     * 
     * Why do this? scalaibity and easy to modify because all would be in one place.
     */


    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            return Ok(await _dbContext.Players.Include(x => x.PlayerSkills).ToListAsync());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        try
        {
            return Ok(await _dbContext.Players.Include(x => x.PlayerSkills).FirstOrDefaultAsync(x => x.Id == id));
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] Player player)
    {
        try
        {
            await _dbContext.Players.AddAsync(player);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.Players.FindAsync(player.Id));
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute] int id, Player updatedPlayer)
    {
        try
        {
            var player = await _dbContext.Players.Include(x => x.PlayerSkills).FirstOrDefaultAsync(x => x.Id == id);

            player.Name = updatedPlayer.Name;
            player.Position = updatedPlayer.Position;
           
            await _dbContext.SaveChangesAsync();

            return Ok(player);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            _ = _dbContext.Players.Remove(await _dbContext.Players.FindAsync(id));
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
