using Microsoft.EntityFrameworkCore;

using Archery.Model;

namespace Archery.Repository;

public class UserRepository : AbstractRepository
{
    public UserRepository(ArcheryContext context) : base(context) { }
    
    public IEnumerable<User> GetAllUsers() => Context.User.AsNoTracking().ToList();
}
