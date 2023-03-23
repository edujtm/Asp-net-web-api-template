using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Enums;

namespace Entities.Models
{
    public class Vehicle
    {
        [Column("VehicleId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "An current miliage for this vehicle is a required field.")]
        public int Mileage { get; set; }

        [Required(ErrorMessage = "An final date of booking is a required field.")]
        [MaxLength(255, ErrorMessage = "Maximum length for the Model's Name is 255 characters.")]
        public string? Model { get; set; }

        [Required(ErrorMessage = "Manufactures's name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Manufactures's Name is 50 characters.")]
        public string? Manufacturer { get; set; }

        [Required(ErrorMessage = "Category is a required field.")]
        [EnumDataType(typeof(VehicleCategoryEnum))]
        public int Category { get; set; }

        [Required(ErrorMessage = "Color is a required field.")]
        [EnumDataType(typeof(VehicleColorEnum))]
        public int Color { get; set; }

        [Required(ErrorMessage = "Daily rental fee is a required field.")]
        public double DailyRentalFee { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
