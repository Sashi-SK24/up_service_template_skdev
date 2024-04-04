using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WorkerServiceTemplate.Models
{   //Weather 
    public class Weather
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(-20.0, 55.0, ErrorMessage = "Must be between -20 and 55")]
        public float Temperature { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Summary { get; set; }
    }

    //Send Email Notif
    public class Notif
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        public string SMTP_Host { get; set; }


        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public int SMTP_Port { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        public string SMTP_Username { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        public string SMTP_Password { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        public string Sender_Email { get; set; }
    }
}
