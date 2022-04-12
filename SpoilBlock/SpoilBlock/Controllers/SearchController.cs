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
        private IAddMediaService _addMediaService;

        public SearchController(IIMDbSearchService searchService, IAddMediaService addService)
        {
            _searchService = searchService;
            _addMediaService = addService;
        }

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


        //[HttpGet]
        //public async Task<IActionResult> Search(SearchViewModel model)
        //{
        //    if (model.query == null) return View();

        //    try
        //    {
        //        var searchResults = await _searchService.GetSearchResultsAsync(model.query);
        //        model.resultsList = searchResults.Item1;
        //        model.errorMessage = searchResults.Item2;
        //    }
        //    catch (ArgumentNullException e)
        //    {
        //        Console.WriteLine(e.Message);
        //        model.errorMessage = "We're having difficulty processing the results from IMDb.  Please try again later.";
        //    }
        //    catch (ArgumentException e)
        //    {
        //        Console.WriteLine(e.Message);
        //        model.errorMessage = "We're having difficulty processing the results from IMDb.  Please try again later.";
        //    }
        //    catch(InvalidJSONException e)
        //    {
        //        Console.WriteLine(e.Message);
        //        model.errorMessage = "We're having difficulty processing the results from IMDb.  Please try again later.";
        //    }
        //    catch(HttpRequestException e)
        //    {
        //        Console.WriteLine(e.Message);
        //        model.errorMessage = "We're having difficulty connecting to the IMDb server. Please check your internet connection or try again later.";
        //    }
        //    catch(Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        model.errorMessage = "Sorry, something went wrong.  Please try again later.";
        //    }

        //    return View(model);
        //}


        [Authorize]
        [HttpPost]
        public IActionResult Add(SearchViewModel model)
        {

            try
            {
                _addMediaService.Add(model.addSelectionId, model.addSelectionTitle, model.addSelectionDescription);
            } catch(Exception e)
            {

            }
            return RedirectToAction("Index");
        }
    }
}
