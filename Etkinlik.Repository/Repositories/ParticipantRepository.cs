
using Etkinlik.Core.Models;
using Etkinlik.Core.Repositories;
using Etkinlik.Repository.Context;
using Etkinlik.Repository.Repositories;
namespace Repository.Repositories
{
    public class ParticipantRepository: GenericRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(AppDbContext context) : base(context)
        {
        }


    }
}
