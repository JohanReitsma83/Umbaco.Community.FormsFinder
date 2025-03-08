using System.ComponentModel.DataAnnotations;

namespace Umbraco.Community.FormsFinder.Core.Models;

public class FormUsageInformation
{
    [Required]
    public required FormInformation Form { get; set; }
    [Required]
    public List<PageInformation> Pages { get; set; } = [];
}


public class FormsUsage
{
    public FormUsageInformation[] Forms { get; set; } = [];

    public Paging Paging
    {
        get;
        set;
    } = new Paging() { CurrentPage = 1, TotalPages = 1 };

}
