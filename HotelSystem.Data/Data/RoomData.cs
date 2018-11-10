using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSystem.Data.Data
{
    public class RoomData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string RoomNumber { get; set; }

        [Required]
        public int RoomTypeDataId { get; set; }

        public RoomTypeData RoomTypeData { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool OnPromotion { get; set; }

        [Required]
        public decimal OnPromotionPrice { get; set; }

        [Required]
        public bool IsOccupied { get; set; }

        public DateTime BookedFrom { get; set; }

        public DateTime BookedTo { get; set; }

        public ICollection<GuestData> Guests { get; set; }
    }
}
