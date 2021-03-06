using System.ComponentModel.DataAnnotations;

namespace ToksApi.Models.ServiceModels
{
    public class IssueModifedModel
    {

        [Required]
        [StringLength(250)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(2500)]
        public string Description { get; set; } = string.Empty;
        public IssueType IssueType { get; set; }
        public Priority Priority { get; set; }

    }
}
