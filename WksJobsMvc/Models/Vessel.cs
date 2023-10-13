using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WksJobsMvc.Models
{
    [Index(nameof(VesselCode), IsUnique = true)]
    public class Vessel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Vessel code")]
        public string VesselCode { get; set; }

        [Required]
        [Display(Name = "Vessel type")]
        public string VesselType { get; set; }

        [Required]
        [Display(Name = "Vessel name")]
        public string VesselName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}
