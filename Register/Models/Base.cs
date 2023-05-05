namespace Register.Models
{
    public class Base
    {
        public bool IsActive { get; set; }

        public string? Createdby { get; set; }
        public string? CreatedOn { get; set; }

        public string? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
