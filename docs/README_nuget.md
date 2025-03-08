# Umbraco.Community.FormsFinder 
[![Downloads](https://img.shields.io/nuget/dt/Umbraco.Community.FormsFinder?color=cc9900)](https://www.nuget.org/packages/Umbraco.Community.FormsFinder/)
[![NuGet](https://img.shields.io/nuget/vpre/Umbraco.Community.FormsFinder?color=0273B3)](https://www.nuget.org/packages/Umbraco.Community.FormsFinder)
[![GitHub license](https://img.shields.io/github/license/JohanReitsma83/Umbaco.Community.FormsFinder?color=8AB803)](../LICENSE)

**Umbraco Forms Finder**  
This package uses Umbraco's internal index to quickly find all forms across your content. 

It shows a list of each form and the pages where they're used, making it easy to track forms on your site. 
Perfect for Umbraco developers and content editors, this tool helps you manage forms by giving you a clear view of where they're being used, so you can keep everything organized. 

The user only sees the forms they have access to, so you can be sure that the information is secure.
Also if the user can't see the content the page is shown as red in the Backoffice so you can't navigate to it directly.

If you do have access to the forms and nodes you can visit the form by clicking the name.
The link to the content will open in a new tab so you can easily navigate back to the list.
And last you can see if the page is published or not. (keep in mind it doesn't know for sure the form is already online)
but if somebody is gonna publish the page the form will be online that why we are showing the status.

Basic user showing forms and content
<img alt="Basic user showing forms and content" src="https://github.com/JohanReitsma83/Umbaco.Community.FormsFinder/blob/contrib/v13/docs/formsfinder.png">



## Installation

Add the package to an existing Umbraco website (v13+) from NuGet:

`dotnet add package Umbraco.Community.FormsFinder`

The package is pull and play, so install and you will see a dashboard in the forms section called Forms Finder.
If you don't see any forms first check if you have edit access to the forms, and second rebuild the internal index.


## Contributing

Contributions to this package are most welcome! Please read the [Contributing Guidelines](CONTRIBUTING.md).
