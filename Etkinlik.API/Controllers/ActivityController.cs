
using AutoMapper;
using Etkinlik.API.ViewModels;
using Etkinlik.Core.DTOs;
using Etkinlik.Core.Models;
using Etkinlik.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Etkinlik.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer",Roles ="Admin,User")]
    
    public class ActivityController : CustomBaseController
    {
        protected IService<Activity> service;
        protected IMapper mapper;
        protected IActivityService activityService;
        protected IParticipantService participantService;
        public ActivityController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager = null, RoleManager<AppRole> roleManager = null, IService<Activity> service = null, IMapper mapper = null, IActivityService activityService = null, IParticipantService participantService = null) : base(userManager, signInManager, roleManager)
        {
            this.service = service;
            this.mapper = mapper;
            this.activityService = activityService;
            this.participantService = participantService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var activities = await service.GetAllAsync();
            //var result= service.Where(x => x.IsActive == true);
            var activitiesDto = mapper.Map<List<ActivityDto>>(activities.ToList());
            return CreateActionResult(CustomResponseDto<List<ActivityDto>>.Success(200, activitiesDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            
            return CreateActionResult(await activityService.GetActivitiesById(id));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetActivityWithDetails()
        {
            return CreateActionResult(await activityService.GetActivitytWithDetails());
        }

        [HttpPost]
        public async Task<IActionResult> Save(ActivityDto activityDto)
        {
            ;
            AppUser user = await userManager.FindByEmailAsync(activityDto.UserEmail);
            

            activityDto.UserId = user.Id;

            var activity = await activityService.AddAsync(mapper.Map<Activity>(activityDto));
            
            var activitiesDto = mapper.Map<ActivityDto>(activity);
            return CreateActionResult(CustomResponseDto<ActivityDto>.Success(201, activitiesDto));
        }


       
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> JoinActivity(JoinActivityViewModel joinActivityViewModel)
        {

            AppUser user = await userManager.FindByEmailAsync(joinActivityViewModel.UserEmail);
            string userid = user.Id;

            var result= activityService.JoinActivityAsync(joinActivityViewModel.ActivityId);
            if (result.Quota!=0)
            {
                result.Quota = result.Quota - 1;
                await activityService.UpdateAsync(result);
                ParticipantDto participantDto = new();
                participantDto.ActivityId = result.Id;
                participantDto.UserId = userid;
                var participantResult = mapper.Map<Participant>(participantDto);
                await participantService.UpdateAsync(participantResult);
            }
                        
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(201));
        }
    }
}
