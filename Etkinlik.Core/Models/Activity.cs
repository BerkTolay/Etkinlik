using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Etkinlik.Core.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime LastDate { get; set; }
        public string Description { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public int Quota { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public bool IsFree { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int? TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public bool ApprovedByAdmin { get; set; }
        public string? RejectReason { get; set; }
    }
}
