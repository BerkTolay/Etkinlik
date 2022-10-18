using Etkinlik.Core.DTOs;
using Etkinlik.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace Etkinlik.MVC.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public async Task<IActionResult> Index()
        {

            var check = HttpContext.Session.GetString("JWToken");
            if (check == null)
            {
                return RedirectToAction("Login", "Member");
            }
            var httpclient = new HttpClient();



            var accessToken = HttpContext.Session.GetString("JWToken");

            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);



            var result = await httpclient.GetFromJsonAsync<CustomResponseDto<List<ActivityWithAllDetails>>>("https://localhost:7256/api/Activity/GetActivityWithDetails");

            //ERROR eklenecek

            return View(result.Data);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}