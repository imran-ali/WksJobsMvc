using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WksJobsMvc.Models
{
    public class JobItemDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Item code")]
        public string ItemCode { get; set; }

        [Required]
        [Display(Name = "Item qty")]
        public float ItemQty { get; set; }

        [Required]
        [Display(Name = "Item rate")]
        public float ItemRate { get; set; }

        [Required]
        [Display(Name = "Net total")]
        public float ItemNetTotal { get; set; }
                
        public int JobId { get; set; }
        public int ItemId { get; set; }

        public ICollection<Job>? Jobs { get; set; }
        public ICollection<Item>? Items { get; set; }

    }
}
