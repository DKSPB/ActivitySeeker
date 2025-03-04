using System.Data;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Utils;
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

    public async Task<List<City>> GetCities()
    {
        return await _context.Cities.ToListAsync();
    }
    
    /// <inheritdoc />
    public async Task UploadImage(int cityId, string path, Stream image)
    {
        var city = await GetById(cityId);

        if (city is null)
        {
            throw new NoNullAllowedException("Не найден город по заданному идентификатору");
        }

        city.ImagePath = path;

        await FileProvider.UploadImage(path, image);

        _context.Cities.Update(city);

        await _context.SaveChangesAsync();
    }
}