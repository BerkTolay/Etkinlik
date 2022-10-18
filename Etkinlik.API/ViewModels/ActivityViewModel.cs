namespace Etkinlik.API.ViewModels
{
    public class ActivityViewModel
    {
        public string UserEmail { get; set; }
        public string? UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime LastDate { get; set; }
        public string Description { get; set; }
        public string CityName { get; set; }
        public string CategoryName { get; set; }
        public string Address { get; set; }
        public int QuotaValue { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool ApprovedByAdmin { get; set; }
        public string? RejectReason { get; set; }
    }
}
