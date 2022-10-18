using AutoMapper;
using Etkinlik.API.Helper;
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
    public class TicketSellersController : ControllerBase
    {
        protected readonly ITokenHandler tokenHandler;
        protected ICompanyService companyService;
        protected IAuthService authService;
        protected IService<Activity> service;
        protected IMapper mapper;


        public TicketSellersController(ITokenHandler tokenHandler, ICompanyService companyService, IAuthService authService, IService<Activity> service, IMapper mapper)
        {
            this.tokenHandler = tokenHandler;
            this.companyService = companyService;
            this.authService = authService;
            this.service = service;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(CompanyViewModel companyViewModel)
        {
            
            Company company = new Company();
            company.DomainName = companyViewModel.DomainName;
            company.Name=companyViewModel.Name;
            company.Password=companyViewModel.Password;
            await companyService.AddAsync(company);

            var result= await companyService.GetByName(companyViewModel.Name);
            

            var response=await authService.SingUp(result);
            return CreateActionResult(response);

        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Company")]
        [HttpGet]
        public IActionResult GetTicketList()
        {
            var activities = service.Where(x => x.IsFree == true);
            var activitiesDto = mapper.Map<List<ActivitiesForSellersDto>>(activities.ToList());
            return CreateActionResult(CustomResponseDto<List<ActivitiesForSellersDto>>.Success(200, activitiesDto));
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
