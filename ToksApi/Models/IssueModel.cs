using System.ComponentModel.DataAnnotations;

namespace ToksApi.Models
{
    public class IssueModel
    {
        public uint Id { get; set; }



        [Required]
        [StringLength(250)]
        public string Title { get; set; } = string.Empty;



        [Required]
        [StringLength(2500)]
        public string Description { get; set; } = string.Empty;
        public IssueType IssueType { get; set; }
        public Priority Priority { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Completed { get; set; }
    }
}
