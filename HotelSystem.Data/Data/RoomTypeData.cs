using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSystem.Data.Data
{
    public class RoomTypeData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int NumberOfBeds { get; set; }

        [Required]
        public bool HasBalcony { get; set; }

        [Required]
        public bool IsDisabilityFriendly { get; set; }

        public ICollection<RoomData> RoomData { get; set; }
    }
}
