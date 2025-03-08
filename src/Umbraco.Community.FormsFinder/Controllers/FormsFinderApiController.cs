using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Community.FormsFinder.Core.Services;

namespace Umbraco.Community.FormsFinder.Controllers;

[PluginController(Constants.Prefix)]
public class FormsFinderApiController(IFormFinderService formFinderService) : UmbracoAuthorizedJsonController
{
    [HttpGet]
    public Task<ActionResult> Get(int page)
    {
        var forms = formFinderService.GetFormsUsageInformation(page);
		return Task.FromResult<ActionResult>(new JsonResult(forms));
	}

    [HttpGet]
    public Task<ActionResult> GetFormsWithName(int page, string name)
    {
        var forms = formFinderService.GetFormsUsageInformation(page, name);
        return Task.FromResult<ActionResult>(new JsonResult(forms));
    }

}
