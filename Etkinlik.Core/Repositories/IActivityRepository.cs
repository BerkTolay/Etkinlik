using Etkinlik.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etkinlik.Core.Repositories
{
    public interface IActivityRepository : IGenericRepository<Activity>
    {
        Task<List<Activity>> GetActivityWithDetailsAsync();
        Task<List<Activity>> GetActivitiesById(string id);
        Activity GetById(int id);
    }
}
