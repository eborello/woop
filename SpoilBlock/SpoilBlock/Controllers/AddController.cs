using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SpoilBlock.DAL.Abstract;
using SpoilBlock.DAL.Concrete;
using SpoilBlock.Models;
using SpoilBlock.Models.ViewModels;
using Newtonsoft.Json;
using System.Security.Claims;

namespace SpoilBlock.Controllers
{
    public class AddController : Controller
    {
        private readonly IWoopuserRepository _woopUserRepository;
        private readonly IMediumRepository _mediumRepository;
        private readonly IWoopUserMediumRepository _woopUserMediumRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddController(IWoopuserRepository woopuserRepo, IMediumRepository mediumRepo, IWoopUserMediumRepository woopUserMediumRepo, IHttpContextAccessor httpContextAccessor)
        {
            _woopUserRepository = woopuserRepo;
            _mediumRepository = mediumRepo;
            _woopUserMediumRepository = woopUserMediumRepo;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<JsonResult> GetCurrentWatchListIDs()
        {

            try
            {
                var currentUserID = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var currentUser = await _woopUserRepository.GetWoopUserByIdentityIdAsync(currentUserID);

                var watchlist = _woopUserMediumRepository.GetListOfShowsForUser(currentUser.Id);

                List<string> resultsList = new List<string>();

                foreach (Medium medium in watchlist)
                    resultsList.Add(medium.Imdbid);

                string resultsListJSON = JsonConvert.SerializeObject(resultsList);

                return Json(new { success = true, message = "OK", list = resultsListJSON });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<JsonResult> AddMediaToWatchlist(string media)
        {
            try
            {
                var userIdentityId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Woopuser user = await _woopUserRepository.GetWoopUserByIdentityIdAsync(userIdentityId);

                Medium selectedMedia = JsonConvert.DeserializeObject<Medium>(media);

                if (!_mediumRepository.ExistsByImdbID(selectedMedia.Imdbid))
                {
                    selectedMedia = _mediumRepository.AddOrUpdate(selectedMedia);
                }

                selectedMedia = _mediumRepository.FindByImdbID(selectedMedia.Imdbid);

                WoopuserMedium woopuserMedium = new WoopuserMedium { BlockageLevel = 0, Media = selectedMedia, User = user };

                _woopUserMediumRepository.AddOrUpdate(woopuserMedium);

                return await GetCurrentWatchListIDs();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<JsonResult> DeleteShowFromWatchlist(string id)
        {
            try
            {
                var userIdentityId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Woopuser user = await _woopUserRepository.GetWoopUserByIdentityIdAsync(userIdentityId);

                int selectedUserMediaId = _woopUserMediumRepository.GetAll()
                                                                   .Where(um => um.MediaId == int.Parse(id) && um.UserId == user.Id)
                                                                   .FirstOrDefault().Id;

                _woopUserMediumRepository.DeleteById(selectedUserMediaId);

                return Json(new { success = true, message = "OK", id = selectedUserMediaId});
            }
            catch(Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
