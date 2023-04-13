namespace Archery.Model.ApiHelper
{
    public class NewEvent
    {
        public string Name { get; set; } = null!;

        public int ParcourId { get; set; }
        
        public List<int> UserIds { get; set; } = new();
    }
}
