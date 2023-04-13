namespace Archery.Model.ApiHelper
{
    public class AdminViewUser
    {
        public string NickName { get; set; } = null!;
        
        public int Score { get; set; }

        public IEnumerable<Target> Targets { get; set; } = null!;
    }
}