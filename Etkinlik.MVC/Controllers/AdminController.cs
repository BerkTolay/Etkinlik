
using Etkinlik.Core.DTOs;
using Etkinlik.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Etkinlik.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        
        public IActionResult Index()
        {
            var httpclient = new HttpClient();

            if (User.Identity.IsAuthenticated)
                {
                    var accessToken = User.Claims.FirstOrDefault(x => x.Type == "access_token").Value;
                httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                }
            httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


              
            var reqq = httpclient.GetFromJsonAsync<CustomResponseDto<List<ActivityViewModel>>>("https://localhost:7256/api/Activity").Result;

            return View(reqq.Data.Where(x=>x.ApprovedByAdmin==null).ToList());
        }
        
        public IActionResult Reject(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public async Task<IActionResult> Accept(int id)
        {
            ConfirmActivityViewModel confirmActivityViewModel = new();
            confirmActivityViewModel.Id = id;
            confirmActivityViewModel.Status = true;

            if (ModelState.IsValid)
            {
                var client = new HttpClient();
                
                    if (User.Identity.IsAuthenticated)
                    {
                        var accessToken = User.Claims.FirstOrDefault(x => x.Type == "access_token").Value;
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    }
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                      
                    var result = await client.PostAsJsonAsync("https://localhost:7256/api/Admin/ConfirmActivity", confirmActivityViewModel);

                      //ERROR EKLENECEK            
                
            }
            return RedirectToAction("Index");
           
        }
        
        [HttpPost]
        public IActionResult Reject(ConfirmActivityViewModel confirmActivityViewModel)
        {
            var httpclinet = new HttpClient();
            var reqq = httpclinet.PostAsJsonAsync<ConfirmActivityViewModel>("https://localhost:7256/api/Admin/ConfirmActivity", confirmActivityViewModel).Result;
            return View();
        }

    }
}
