using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace ContactsManager.Controllers
{
    [Route("[controller]")]
    public class CountriesController : Controller
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        [Route("UploadFromExcel")]
        [HttpGet]
        public IActionResult UploadFromExcel()
        {
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> UploadFromExcel(IFormFile excelFile)
        {
            if(excelFile == null || excelFile.Length == 0)
            {
                ViewBag.ErrorMessage = "Please select a correct file";
                return View();
            }
            else if(!Path.GetExtension(excelFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.ErrorMessage = "inccorect file having other extension";
                return View();
            }
            int count = await _countriesService.UploadCountriesFromExcelFile(excelFile);
            ViewBag.Message = $"{count} Countries Uploaded";
            return View();
        }
    }
}
