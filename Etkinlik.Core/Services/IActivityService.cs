

using Etkinlik.Core.DTOs;
using Etkinlik.Core.Models;

namespace Etkinlik.Core.Services
{
    public interface IActivityService:IService<Activity>
    {
        Task<CustomResponseDto<List<ActivityDto>>> GetActivitiesById(string id);
        Task<CustomResponseDto<List<ActivityWithAllDetails>>> GetActivitytWithDetails();
        Activity JoinActivityAsync(int activityId);
    }
}
