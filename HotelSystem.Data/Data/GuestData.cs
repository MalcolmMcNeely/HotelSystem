using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSystem.Data.Data
{
    public class GuestData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string AddressLineOne { get; set; }

        public string AddressLineTwo { get; set; }

        [Required]
        public string PostCode { get; set; }

        [Required]
        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string CreditCardNumber { get; set; }

        [Required]
        public decimal AmountOwed { get; set; }

        [Required]
        public decimal AmountPaid { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }
    }
}
