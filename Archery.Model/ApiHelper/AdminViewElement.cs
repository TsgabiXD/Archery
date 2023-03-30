namespace Archery.Model.ApiHelper
{
    public class AdminViewElement
    {
        public string EventName { get; set; } = null!;
        public List<AdminViewUser> User { get; set; } = new();
    }
}