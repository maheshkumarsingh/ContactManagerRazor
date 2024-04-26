using ContactsManager.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTOs;
using ServiceContracts.Enums;

namespace ContactsManager.Filters.ActionFilters
{
    public class PersonListActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<PersonListActionFilter> _logger;

        public PersonListActionFilter(ILogger<PersonListActionFilter> logger)
        {
            _logger = logger;
        }
        #region Old IActionFilter
        /*
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"{nameof(PersonListActionFilter)}.{nameof(OnActionExecuted)} method");
            PersonsController personsController = (PersonsController)context.Controller;

            IDictionary<string, object?>? parameters = (IDictionary<string, object?>?)context.HttpContext.Items["arguments"];
            if (parameters != null)
            {
                if (parameters.ContainsKey("searchBy"))
                    personsController.ViewData["CurrentSearchBy"] = Convert.ToString(parameters["searchBy"]);
                if (parameters.ContainsKey("searchString"))
                    personsController.ViewData["SeachString"] = Convert.ToString(parameters["searchString"]);
                if (parameters.ContainsKey("sortBy"))
                    personsController.ViewData["CurrentSortBy"] = Convert.ToString(parameters["sortBy"]);
                if (parameters.ContainsKey("sortOrder"))
                    personsController.ViewData["CurrentSortOrderOptionType"] = Convert.ToString(parameters["sortOrder"]);
            }
            personsController.ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {nameof(PersonResponseDTO.PersonName), "Person Name"},
                {nameof(PersonResponseDTO.Email), "Email"},
                {nameof(PersonResponseDTO.Gender), "Gender"},
                {nameof(PersonResponseDTO.Address), "Address"},
                {nameof(PersonResponseDTO.CountryName), "Country"},
            };
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items["arguments"] = context.ActionArguments;
            _logger.LogInformation($"{nameof(PersonListActionFilter)}.{nameof(OnActionExecuting)} method");

            //To do: and before logic
            if (context.ActionArguments.ContainsKey("searchBy"))
            {
                string? searchBy = Convert.ToString(context.ActionArguments["searchBy"]);
                _logger.LogInformation($"SearchBy actual value {searchBy}", searchBy);
                if (!string.IsNullOrEmpty(searchBy))
                {
                    var searchByOptions = new List<string>()
                    {
                        nameof(PersonResponseDTO.PersonName),
                        nameof(PersonResponseDTO.Email),
                        nameof(PersonResponseDTO.DOB),
                        nameof(PersonResponseDTO.Gender),
                        nameof(PersonResponseDTO.CountryName),
                        nameof(PersonResponseDTO.Address)
                    };
                    if (searchByOptions.Any(temp => temp == searchBy) == false)
                    {
                        context.ActionArguments["searchBy"] = nameof(PersonResponseDTO.PersonName);
                        _logger.LogInformation($"SearchBy actual value {searchBy}", searchBy);
                    }
                }
            }
        }
        */
        #endregion
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.Items["arguments"] = context.ActionArguments;
            _logger.LogInformation($"Before {nameof(PersonListActionFilter)}.{nameof(OnActionExecutionAsync)} method");

            //To do: and before logic
            if (context.ActionArguments.ContainsKey("searchBy"))
            {
                string? searchBy = Convert.ToString(context.ActionArguments["searchBy"]);
                _logger.LogInformation($"SearchBy actual value {searchBy}", searchBy);
                if (!string.IsNullOrEmpty(searchBy))
                {
                    var searchByOptions = new List<string>()
                    {
                        nameof(PersonResponseDTO.PersonName),
                        nameof(PersonResponseDTO.Email),
                        nameof(PersonResponseDTO.DOB),
                        nameof(PersonResponseDTO.Gender),
                        nameof(PersonResponseDTO.CountryName),
                        nameof(PersonResponseDTO.Address)
                    };
                    if (searchByOptions.Any(temp => temp == searchBy) == false)
                    {
                        context.ActionArguments["searchBy"] = nameof(PersonResponseDTO.PersonName);
                        _logger.LogInformation($"SearchBy actual value {searchBy}", searchBy);
                    }
                }
            }
            await next();

            _logger.LogInformation($"After {nameof(PersonListActionFilter)}.{nameof(OnActionExecutionAsync)} method");
            PersonsController personsController = (PersonsController)context.Controller;

            IDictionary<string, object?>? parameters = (IDictionary<string, object?>?)context.HttpContext.Items["arguments"];
            if (parameters != null)
            {
                if (parameters.ContainsKey("searchBy"))
                    personsController.ViewData["CurrentSearchBy"] = Convert.ToString(parameters["searchBy"]);
                if (parameters.ContainsKey("searchString"))
                    personsController.ViewData["CurrentSearchString"] = Convert.ToString(parameters["searchString"]);
                if (parameters.ContainsKey("sortBy"))
                    personsController.ViewData["CurrentSortBy"] = Convert.ToString(parameters["sortBy"]);
                else
                    personsController.ViewData["CurrentSortBy"] = nameof(PersonResponseDTO.PersonName);
                if (parameters.ContainsKey("sortOrder"))
                    personsController.ViewData["CurrentSortOrderOptionType"] = Convert.ToString(parameters["sortOrder"]);
                else
                    personsController.ViewData["CurrentSortOrderOptionType"] = nameof(SortOrderOptionType.Ascending);
            }
            personsController.ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {nameof(PersonResponseDTO.PersonName), "Person Name"},
                {nameof(PersonResponseDTO.Email), "Email"},
                {nameof(PersonResponseDTO.Gender), "Gender"},
                {nameof(PersonResponseDTO.Address), "Address"},
                {nameof(PersonResponseDTO.CountryName), "Country"},
            };
        }
    }
}
