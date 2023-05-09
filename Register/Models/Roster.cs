using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Register.Enum.Enums;

namespace Register.Models
{
    public class Roster : Base
    {
        [Key]
        public Guid RosterId { get; set; }

        public eStatus Status { get; set; }

        public Guid PersonId { get; set; }
    }
}
