using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Models
{
    public class Appointments
    {
        public int Id { get; set; }

        
        public DateTime AppointmentDate { get; set; }

        [NotMapped]
        public DateTime AppointmentTime { get; set; }

        [Required(ErrorMessage = "{0} required!")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Filed is mandatory!")]
        public string CustomerPhoneNumber { get; set; }

        [Required(ErrorMessage = "Filed is mandatory!")]
        [EmailAddress(ErrorMessage = "Email not valid!")]
        public string CustomerEmail { get; set; }

        public bool isConfirmed { get; set; }
    }
}
