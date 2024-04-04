using System.ComponentModel.DataAnnotations;

namespace WorkerServiceTemplate.Dto
{
    public class CreateWeatherRequestDTO
    {
        public string Name { get; set; }
        public float Temperature { get; set; }
        public string Summary { get; set; }
    }
}
