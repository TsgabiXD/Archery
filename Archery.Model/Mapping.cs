namespace Archery.Model
{
    public class Mapping
    {
        public int Id { get; set; }
        public Event Event { set; get; } = null!;
        public User? User { set; get; }
        public List<Target> Target { set; get; } = new();
    } 
}
