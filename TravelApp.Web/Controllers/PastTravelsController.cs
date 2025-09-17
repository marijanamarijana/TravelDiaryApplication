using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelApp.Service.Implementation;
using TravelApp.Service.Interface;

namespace TravelApp.Web.Controllers
{
    public class PastTravelsController : Controller
    {
        private readonly IPastTravelsService _pastTravelsService;

        public PastTravelsController(IPastTravelsService pastTravelsService)
        {
            _pastTravelsService = pastTravelsService;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return View(_pastTravelsService.GetUserPastTravelsObjects(userId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToPastTravels(Guid travelId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            _pastTravelsService.AddToPastTravels(travelId, userId);

            return RedirectToAction("Index", "Travels");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromPastTravels(Guid travelId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            _pastTravelsService.RemoveFromPastTravels(travelId, userId);

            return RedirectToAction("Index");
        }
    }
}
