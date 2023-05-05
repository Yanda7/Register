using System.ComponentModel.DataAnnotations;
using static Register.Enum.Enums;

namespace Register.Models
{
    public class Person : Base
    {
        [Key]
        public Guid PersonId { get; set; }

        [Required]
        [MinLength(3)]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        public eGender Gender { get; set; }

        [Required]
        public eTitle Title { get; set; }

        [MinLength(1)]
        [MaxLength(13)]
        [Display(Name ="ID Number")]
        public string IdNumber { get; set; }    

        

    }
}
