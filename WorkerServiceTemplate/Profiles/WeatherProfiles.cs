using AutoMapper;
using WorkerServiceTemplate.Dto;
using WorkerServiceTemplate.Models;

namespace WorkerServiceTemplate.Profiles
{
    public class WeatherProfiles : Profile
    {
        public WeatherProfiles() 
        {
            CreateMap<CreateWeatherRequestDTO, Weather>();
        }
    }
}
