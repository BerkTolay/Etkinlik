using Etkinlik.Core.IUnitOfWork;
using Etkinlik.Core.Models;
using Etkinlik.Core.Repositories;
using Etkinlik.Core.Services;

namespace Etkinlik.Service.Services
{
    public class ParticipantService : Service<Participant>, IParticipantService
    {
        public ParticipantService(IGenericRepository<Participant> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
