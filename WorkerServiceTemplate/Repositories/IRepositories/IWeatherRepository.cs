using System.Linq.Expressions;
using WorkerServiceTemplate.Models;

namespace WorkerServiceTemplate.Repositories.IRepositories
{
    public interface IWeatherRepository
    {
        Task<List<Weather>> GetAllAsync(CancellationToken token, Expression<Func<Weather, bool>> filter = null);
        Task<Weather> GetAsync(Expression<Func<Weather, bool>> filter = null, bool tracked = true);
        Task<Weather> CreateAsync(Weather weather);
        Task<bool> UpdateAsync(Weather weather);
        Task<bool> DeleteAsync(Guid weatherId);
        Task<bool> CheckIfExist(Guid weatherId);
    }
}
