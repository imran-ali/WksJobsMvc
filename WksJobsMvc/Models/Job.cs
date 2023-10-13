using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WksJobsMvc.Models
{
    public class Job
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Job month")]
        public string Month { get; set; }

        [Required]
        [Column("JobCode")]
        [Display(Name = "Job code")]
        public string JobCode { get; set; }


        [Required]
        [Column("JobType")]
        [Display(Name = "Job type")]
        public string JobType { get; set; }

        [Required]
        [Display(Name = "Job title")]
        public string JobTitle { get; set; }

        [Required]
        [Display(Name = "Job description")]
        public string? JobDescription { get; set; }

        [Required]
        [Display(Name = "Number of persons")]
        public int NumberOfPersons { get; set; }

        [Required]
        [Display(Name = "Hours worked")]
        public int HoursWorked { get; set; }

        [Required]
        [Display(Name = "Number of days")]
        public int NoOfDays { get; set; }

        [Required]
        [Display(Name = "Third party service cost")]
        public int ThirdPartyServiceCost { get; set; }

        [Required]
        [Display(Name = "Job status")]
        public string JobStatus { get; set; } = "Draft";

    }
}
