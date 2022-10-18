using Etkinlik.Core.Models;
using Etkinlik.Core.Repositories;
using Etkinlik.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Etkinlik.Repository.Repositories
{
    public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<List<Activity>> GetActivityWithDetailsAsync()
        {
            return context.Activities.Include(x => x.Ticket).ToList(); //Eager loading
        }
        public async Task<List<Activity>> GetActivitiesById(string id)
        {
            return context.Activities.Where(x => x.User.Id == id).ToList();
        }
        public Activity GetById(int id)
        {
            return context.Activities.Find(id);
        }

    }
}
