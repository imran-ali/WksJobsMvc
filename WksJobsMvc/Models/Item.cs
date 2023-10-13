using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace WksJobsMvc.Models
{
    
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Item code")]
        public string ItemCode { get; set; }

        [Required]
        [Display(Name = "Unit of measurement")]
        public string UnitOfMeasurement { get; set; }

        [Required]
        [Display(Name = "Material description")]
        public string MaterialDescription { get; set; }

        [Required]
        [Display(Name = "Per unit cost")]
        public double PerUnitCost { get; set; }

        [Required]
        [Display(Name = "System Date")]
        public DateTime CreationDate { get; set; } = System.DateTime.Now;


    }
}
