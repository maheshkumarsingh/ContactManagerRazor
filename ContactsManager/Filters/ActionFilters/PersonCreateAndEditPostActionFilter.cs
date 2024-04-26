using ContactsManager.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.DTOs;

namespace ContactsManager.Filters.ActionFilters
{
    public class PersonCreateAndEditPostActionFilter : IAsyncActionFilter
    {
        private readonly ICountriesService _countriesService;
        
        public PersonCreateAndEditPostActionFilter(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Before Logic
            if(context.Controller is PersonsController personsController)
            {
                if (!personsController.ModelState.IsValid)
                {
                    List<CountryResponseDTO> countries = await _countriesService.GetAllCountries();
                    personsController.ViewBag.Countries = countries.Select(temp =>
                           new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() }
                    );
                    personsController.ViewBag.Errors = personsController.ModelState.Values.SelectMany(value => value.Errors).Select(e => e.ErrorMessage).ToList();
                    var personRequest = context.ActionArguments["personRequest"];
                    context.Result = personsController.View(personRequest); //step 1) assign something
                }
            //step2) await next(); short circuit
                else
                {
                    await next();
                }
            }
            else
            {
                await next();
            }
            //After Logic
        }
    }
}
