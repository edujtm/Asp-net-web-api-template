
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models
{
    public class Booking
    {
        [Column("BookingId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "An initial date of booking is a required field.")]
        [DataType(DataType.Date)]
        public DateTime From { get; set; }

        [Required(ErrorMessage = "An final date of booking is a required field.")]
        [DataType(DataType.Date)]
        public DateTime To { get; set; }

        [Required(ErrorMessage = "Status a required field.")]
        [EnumDataType(typeof(Booking))]
        public int Status { get; set; }

        public bool PaymentReceived { get; set; } = false;


        [ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [ForeignKey(nameof(Vehicle))]
        public Guid VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }

    }
}
