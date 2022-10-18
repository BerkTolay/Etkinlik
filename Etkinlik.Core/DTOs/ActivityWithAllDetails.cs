using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etkinlik.Core.DTOs
{
    public class ActivityWithAllDetails:ActivityDto
    {
        public AppUserDto AppUserDto { get; set; }
        public TicketDto TicketDto { get; set; }
    }
}
