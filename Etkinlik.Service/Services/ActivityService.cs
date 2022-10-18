using AutoMapper;
using Etkinlik.Core.DTOs;
using Etkinlik.Core.IUnitOfWork;
using Etkinlik.Core.Models;
using Etkinlik.Core.Repositories;
using Etkinlik.Core.Services;

namespace Etkinlik.Service.Services
{
    public class ActivityService : Service<Activity>, IActivityService
    {
        private readonly IActivityRepository activityRepository;
        private readonly IMapper mapper;
        private readonly IParticipantRepository participantRepository;
        private readonly IUnitOfWork unitOfWork;
        
        public ActivityService(IGenericRepository<Activity> repository, IUnitOfWork unitOfWork, IActivityRepository activityRepository, IMapper mapper, IParticipantRepository participantRepository) : base(repository, unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.activityRepository = activityRepository;
            this.mapper = mapper;
            this.participantRepository = participantRepository;
        }

        public async Task<CustomResponseDto<List<ActivityDto>>> GetActivitiesById(string id)
        {
            var activities = await activityRepository.GetActivitiesById(id);
            var activityDto = mapper.Map<List<ActivityDto>>(activities);
            return CustomResponseDto<List<ActivityDto>>.Success(200, activityDto);
        }

       

        public async Task<CustomResponseDto<List<ActivityWithAllDetails>>> GetActivitytWithDetails()
        {
            var activities = await activityRepository.GetActivityWithDetailsAsync();
            var activityDto=mapper.Map<List<ActivityWithAllDetails>>(activities);
            return CustomResponseDto<List<ActivityWithAllDetails>>.Success(200, activityDto);
        }

        public Activity JoinActivityAsync(int activityId)
        {
            
            
            var result = activityRepository.GetByIdAsync(activityId); 

            return result;
        }
    }
}
