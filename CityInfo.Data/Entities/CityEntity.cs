using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.Data.Entities
{
    [Table("City")]
    public class CityEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public ICollection<PointOfInterestEntity> PointsOfInterest { get; set; }
               = new List<PointOfInterestEntity>();

        public CityEntity(string name)
        {
            Name = name;
        }
    }
}
