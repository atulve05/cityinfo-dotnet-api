using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.Data.Entities
{
    [Table("Product")]
    public class ProductEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public string Category { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
    }
}
