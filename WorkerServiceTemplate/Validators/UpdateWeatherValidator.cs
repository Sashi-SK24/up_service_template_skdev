using FluentValidation;
using WorkerServiceTemplate.Dto;
using WorkerServiceTemplate.Models;

namespace WorkerServiceTemplate.Validators
{
    public class UpdateWeatherValidator : AbstractValidator<UpdateWeatherRequestDTO>
    {
        public UpdateWeatherValidator() 
        {
            RuleFor(weather => weather.Name)
                .Length(3, 50).WithMessage("Name length should be between 3 and 50 characters.");

            RuleFor(weather => weather.Temperature)
               .Must(temperature => temperature > -20.0 && temperature < 50.0)
               .WithMessage("Temperature should be between -20.0 and 55.0.");

            RuleFor(weather => weather.Summary)
                .Length(3, 50).WithMessage("Summary should be between 3 and 50 characters.");
        }
    }
}
