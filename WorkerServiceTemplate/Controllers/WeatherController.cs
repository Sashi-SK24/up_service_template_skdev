using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using System.Net;
using WorkerServiceTemplate.Dto;
using WorkerServiceTemplate.Models;
using WorkerServiceTemplate.Repositories;
using WorkerServiceTemplate.Repositories.IRepositories;

namespace WorkerServiceTemplate.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherRepository> _logger;
        private readonly IMapper _mapper;
        private readonly IWeatherRepository _weatherRepo;

        public WeatherController(IWeatherRepository weatherRepository, IMapper mapper, ILogger<WeatherRepository> logger)
        {
            _weatherRepo = weatherRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("forecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Temperature = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Weather>))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            try
            {
                List<Weather> weatherList = await _weatherRepo.GetAllAsync(token);
                return Ok(weatherList.ToArray());
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest, Constants.MediaType.JSON)]
        public async Task<IActionResult> Create([FromBody] CreateWeatherRequestDTO createWeatherDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newObj = _mapper.Map<CreateWeatherRequestDTO, Weather>(createWeatherDTO);
            Weather newWeatherObj = await _weatherRepo.CreateAsync(newObj);
            return Ok(newWeatherObj);
        }

        [HttpPut("{weatherId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid weatherId, [FromBody] UpdateWeatherRequestDTO updateWeatherRequestDTO)
        {
            try
            {
                bool found = await _weatherRepo.CheckIfExist(weatherId);
                if (!found)
                {
                    return NotFound();
                }

                var updateWeatherObj = new Weather() 
                { 
                    Id = weatherId,
                    Name = updateWeatherRequestDTO.Name,
                    Temperature = updateWeatherRequestDTO.Temperature,
                    Summary = updateWeatherRequestDTO.Summary,
                };

                var response = await _weatherRepo.UpdateAsync(updateWeatherObj);

                if (response)
                {
                    return Ok();
                }
                else 
                {
                    return UnprocessableEntity();
                }

            }
            catch (Exception error)
            {
                _logger.LogError("Error occured while at UpdateWeaatherController. Message: {@error}", error);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("{weatherId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid weatherId)
        {
            Weather matchedWeather = await _weatherRepo.GetAsync(u => u.Id == weatherId);
            if (matchedWeather is null)
            {
                return NotFound();
            }
            return Ok(matchedWeather);
        }


        [HttpDelete("{weatherId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Delete(Guid weatherId)
        {
            try
            {
                bool found = await _weatherRepo.CheckIfExist(weatherId);
                if (!found)
                {
                    return NotFound();
                }

               

                var response = await _weatherRepo.DeleteAsync(weatherId);

                if (response)
                {
                    return Ok();
                }
                else
                {
                    return UnprocessableEntity();
                }

            }
            catch (Exception error)
            {
                _logger.LogError("Error occured while at [Weather] Delete@WeatherController. Message: {@error}", error);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
