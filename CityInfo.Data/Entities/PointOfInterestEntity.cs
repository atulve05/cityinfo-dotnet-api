using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.Data.Entities
{
    [Table("PointOfInterest")]
    public class PointOfInterestEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [ForeignKey("CityId")]
        public CityEntity? City { get; set; }
        public int CityId { get; set; }

        public PointOfInterestEntity(string name)
        {
            Name = name;
        }
    }
}
