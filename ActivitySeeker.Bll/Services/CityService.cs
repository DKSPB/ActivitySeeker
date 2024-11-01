using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain;
using ActivitySeeker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ActivitySeeker.Bll.Services;

public class CityService: ICityService
{
    private readonly ActivitySeekerContext _context;
    
    public CityService(ActivitySeekerContext context)
    {
        _context = context;
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<City>> GetCitiesByName(string name)
    {
        var substring = name
            .ToLower()
            .Trim()
            .Replace(" ", "")
            .Replace("-", "");

        return await _context.Cities.Where(x => x.Name
            .ToLower()
            .Trim()
            .Replace(" ", "")
            .Replace("-", "")
            .Contains(substring)).ToListAsync();
    }

    public async Task<City?> GetById(int id)
    {
       return await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
    }
}