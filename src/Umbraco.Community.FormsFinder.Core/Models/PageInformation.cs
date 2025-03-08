using System.ComponentModel.DataAnnotations;

namespace Umbraco.Community.FormsFinder.Core.Models
{
    public class PageInformation
    {
        [Required]
        public required string? PageTitle { get; set; }

        [Required]
        public required int ContentId { get; set; }

        public bool Published { get; set; }
        public bool HasAccess { get; set; }
    }
}
