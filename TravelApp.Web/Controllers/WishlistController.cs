using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelApp.Service.Interface;

namespace TravelApp.Web.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return View(_wishlistService.GetUserWishlistTravels(userId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToWishlist(Guid travelId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            _wishlistService.AddToWishlist(travelId, userId);

            return RedirectToAction("Index", "Travels");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromWishlist(Guid travelId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            _wishlistService.RemoveFromWishlist(travelId, userId);

            return RedirectToAction("Index");
        }
    }
}
