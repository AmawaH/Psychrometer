using System.ComponentModel.DataAnnotations;

namespace PsychroASP.Models
{
    public class Readings
    {
        [Required(ErrorMessage = "Має бути числом")]
        public double TempDry { get; set; }
        [Required(ErrorMessage = "Має бути числом")]
        public double TempWet { get; set; }
        [Required(ErrorMessage = "Має бути числом")]
        public double Pressure { get; set; }
        [Required]
        public double SpeedWind { get; set; }
        public double Humidity { get; set; }
    }
}
