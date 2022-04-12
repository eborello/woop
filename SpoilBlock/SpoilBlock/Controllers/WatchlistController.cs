using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using SpoilBlock.Models;
using SpoilBlock.DAL.Abstract;
using SpoilBlock.Models.ViewModels;

namespace SpoilBlock.Controllers
{
    public class WatchlistController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWoopuserRepository _woopuserRepository;
        private readonly IWoopUserMediumRepository _woopusermediumRepository;

        public WatchlistController(UserManager<IdentityUser> userManger, IWoopuserRepository woopuserRepository , IWoopUserMediumRepository woopusermediumRepository)
        {
            _userManager = userManger;
            _woopusermediumRepository = woopusermediumRepository;
            _woopuserRepository = woopuserRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            string? id = _userManager.GetUserId(User);
            Woopuser? wum = null;
            if (id != null)
            { 
                wum = _woopuserRepository.GetWoopUserByIdentityId(id);
            }
            if (wum == null) 
                { 
                    WatchlistViewModel empty = new WatchlistViewModel();
                    return View(empty); 
                }

            WatchlistViewModel vm = new WatchlistViewModel()
            {
                HasWoopUser = wum != null,
                Username = wum.Username ?? String.Empty,
                AllShows = _woopusermediumRepository.GetListOfShowsForUser(wum.Id),
                //AllShows = _woopuserRepository.GetListOfShows(_woopusermediumRepository.GetListOfShowsForUser(wum.Id), wum)

            };
            


            if (vm.AllShows.Count() == 0)
            {
                vm.IsEmpty = true;
                return View(vm);
            }else


            return View(vm);
        }



    }
}
