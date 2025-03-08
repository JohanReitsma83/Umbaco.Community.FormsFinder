namespace Umbraco.Community.FormsFinder.Core.Models;

public class FormInformation
{
    public required string[] Path { get; set; }

    public string TotalPath => string.Join("/", Path);
    public required string Guid { get; set; }
    public required string Name { get; set; }
}
