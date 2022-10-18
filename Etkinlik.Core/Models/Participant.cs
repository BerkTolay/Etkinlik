using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etkinlik.Core.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ActivityId { get; set; }
    }
}
