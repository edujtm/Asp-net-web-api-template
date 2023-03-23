using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Customer
    {
        [Column("CustomerId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Customer name is a required field.")]
        [MaxLength(255, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateBirth { get; set; }

        [Required(ErrorMessage = "Age is a required field.")]
        [EnumDataType(typeof(GenderEnum))]
        public int Gender { get; set; }

        [Required(ErrorMessage = "Customer address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters")]
        public string? Address { get; set; }

        [MaxLength(2000, ErrorMessage = "Maximum length for the Details is 2000 characters.")]
        public string? Details { get; set; }

        public ICollection<Booking>? Bookings { get; set; }

    }
}
