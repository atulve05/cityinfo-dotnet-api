using System.ComponentModel.DataAnnotations;

namespace CityInfo.Api.Dto
{
    /// <summary>
    /// The data required to create a point of interest.
    /// </summary>
    public class PointOfInterestForCreationDto
    {
        /// <summary>
        /// The name of the point of interest.
        /// </summary>
        [MaxLength(50)]
        [Required(ErrorMessage = "You should provide a name value.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the point of interest.
        /// </summary>
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
