using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using WorkerServiceTemplate.Models;
using WorkerServiceTemplate.Repositories.IRepositories;

namespace WorkerServiceTemplate.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly WorkerServiceDbContext _db;
        private readonly ILogger<WeatherRepository> _logger;

        public WeatherRepository(WorkerServiceDbContext db, ILogger<WeatherRepository> logger) 
        {
            _db = db;
            _logger = logger;
        }

        public async Task<Weather> CreateAsync(Weather newWeather)
        {
            await _db.Weathers.AddAsync(newWeather);
            await _db.SaveChangesAsync();
            return newWeather;
        }

        public async Task<bool> DeleteAsync(Guid weatherId)
        {
            try
            {
                await _db.Weathers.Where(c => c.Id == weatherId).ExecuteDeleteAsync();
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception error)
            {

                _logger.LogError("Error occured while at [Weather] DeleteAsync@WeatherRepository. Message: {@error}", error);
                return false;
            }
            
        }

        public async Task<List<Weather>> GetAllAsync(CancellationToken token, Expression<Func<Weather, bool>> filter = null)
        {
            IQueryable<Weather> query = _db.Weathers;

            if (filter is not null)
            {
                query = query.Where(filter);
            }
            try
            {
                return await query.ToListAsync(token);
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogInformation("Get weather list cancelled");
            }

            return new List<Weather>();
        }

        public async Task<Weather> GetAsync(Expression<Func<Weather, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Weather> query = _db.Weathers;

            if (!tracked)
            {
                query.AsNoTracking();
            }

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }



        public async Task<bool> UpdateAsync(Weather weather)
        {
            _db.Weathers.Update(weather);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckIfExist(Guid weatherId)
        {
            return _db.Weathers.Any(w => w.Id.Equals(weatherId));
        }
    }
}
