using Etkinlik.API.ViewModels;
using Etkinlik.Core.DTOs;
using Etkinlik.Core.Models;
using Etkinlik.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Etkinlik.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        protected IService<Activity> service;

        public AdminController(IService<Activity> service)
        {
            this.service = service;
        }

        [HttpGet]
        public string Get()
        {
            return "a";
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ConfirmActivity(ConfirmActivityViewModel confirmActivityViewModel)
        {
            var result = service.GetByIdAsync(confirmActivityViewModel.Id);
            result.ApprovedByAdmin = confirmActivityViewModel.Status;
            if (!confirmActivityViewModel.Status)
            {
                result.RejectReason = confirmActivityViewModel.RejectReason;
            }
            await service.UpdateAsync(result);

            return CreateActionResult(CustomResponseDto<ConfirmActivityViewModel>.Success(201, confirmActivityViewModel));
            
       
        }

        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204)//204 no content

                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
        

    }
}
