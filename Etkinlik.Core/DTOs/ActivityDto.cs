
namespace Etkinlik.Core.DTOs
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string? UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime LastDate { get; set; }
        public string Description { get; set; }
        public string CityName { get; set; }
        public string CategoryName { get; set; }
        public string Address { get; set; }
        public int Quota { get; set; }
        public bool IsActive { get; set; }
        public bool IsFree { get; set; }
        public bool ApprovedByAdmin { get; set; }
        public string? RejectReason { get; set; }
    }
}
