using FluentValidation;
using WorkerServiceTemplate.Dto;
using WorkerServiceTemplate.Models;

namespace WorkerServiceTemplate.Validators
{
    public class CreateWeatherValidator : AbstractValidator<CreateWeatherRequestDTO>
    {
        public CreateWeatherValidator() 
        {
            RuleFor(weather => weather.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .Length(3, 50).WithMessage("Name length should be between 3 and 50 characters.");
        }
    }
}
