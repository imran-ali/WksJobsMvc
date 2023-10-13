using System.ComponentModel.DataAnnotations;

namespace WksJobsMvc.Models
{
    public class JobType
    {
        [Key]
        public int Id { get; set; }

        public string JobTypeCode { get; set; }

        public string JobTypeName { get; set; }

    }
}
