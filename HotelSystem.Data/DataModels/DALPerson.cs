using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSystem.Data.DataModels
{
   public class DALPerson
   {
      public DALPerson() { }

      [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      [Required]
      public string Name { get; set; }

      [Required]
      public int Age { get; set; }

      [Required]
      public string AddressLineOne { get; set; }

      [Required]
      public string AddressLineTwo { get; set; }

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

      public void Update(DALPerson other)
      {
         Id = other.Id;
         Name = other.Name;
         Age = other.Age;
         AddressLineOne = other.AddressLineOne;
         AddressLineTwo = other.AddressLineTwo;
         City = other.City;
         PhoneNumber = other.PhoneNumber;
         CreditCardNumber = other.CreditCardNumber;
         AmountOwed = other.AmountOwed;
         AmountPaid = other.AmountPaid;
         DateCreated = other.DateCreated;
         LastUpdated = other.LastUpdated;
      }
   }
}
