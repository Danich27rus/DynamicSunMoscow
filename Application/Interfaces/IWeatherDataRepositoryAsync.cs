using Domain.Entities;

namespace Application.Interfaces;

public interface IWeatherDataRepositoryAsync : IGenericRepositoryAsync<Weather>
{
    Task AddWeathersFromFile(string filepath);
    Task<int> GetCountAsync(); 
}
