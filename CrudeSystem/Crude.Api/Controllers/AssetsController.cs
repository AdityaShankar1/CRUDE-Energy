using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crude.Core.Entities;
using Crude.Infrastructure.Persistence;

namespace Crude.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetsController : ControllerBase
{
    private readonly AppDbContext _context;

    public AssetsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Assets
    [HttpGet]
    public async Task<IActionResult> GetAssets()
    {
        var assets = await _context.Assets.ToListAsync();
        return Ok(assets);
    }

    // POST: api/Assets (The 'C' in CRUDE)
    [HttpPost]
    public async Task<IActionResult> CreateAsset(EnergyAsset asset)
    {
        _context.Assets.Add(asset);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAssets), new { id = asset.Id }, asset);
    }
    // PUT: api/Assets/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsset(Guid id, EnergyAsset updatedAsset)
    {
        if (id != updatedAsset.Id) return BadRequest("ID mismatch");

        _context.Entry(updatedAsset).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Assets.Any(e => e.Id == id)) return NotFound();
            throw;
        }

        return NoContent();
    }

// DELETE: api/Assets/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsset(Guid id)
    {
        var asset = await _context.Assets.FindAsync(id);
        if (asset == null) return NotFound();

        _context.Assets.Remove(asset);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}