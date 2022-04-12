using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SpoilBlock.DAL.Abstract;
using SpoilBlock.DAL.Concrete;
using SpoilBlock.Models;
using SpoilBlock.Models.ViewModels;
using Newtonsoft.Json;

namespace SpoilBlock.Controllers
{
    public class SearchController : Controller
    {
        private IIMDbSearchService _searchService;

        public SearchController(IIMDbSearchService searchService)
        {
            _searchService = searchService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> SearchAjax(string query)
        {
            IEnumerable<IMDbEntry>? resultsList;
            string? errorMessage;
            try
            {
                var searchResults = await _searchService.GetSearchResultsAsync(query);
                resultsList = searchResults.Item1;
                errorMessage = searchResults.Item2;
                if (errorMessage == string.Empty)
                {
                    string resultsListJSON = JsonConvert.SerializeObject(resultsList);
                    return Json(new { success = true, message = "OK", list = resultsListJSON, query = query});
                }

                else 
                    return Json(new { success = false, message = errorMessage });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Json(new { success = false, message = "There was an exception" });
        }
    }
}
