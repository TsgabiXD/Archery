using Microsoft.EntityFrameworkCore;
using Archery.Model;

namespace Archery.Repository;

public class UserRepository : AbstractRepository
{
    public UserRepository(ArcheryContext context) : base(context) { }

    public IEnumerable<User> GetAllUsers()
    {
        return Context.User.AsNoTracking().ToList();
        
    }

    public string AddUser(User user)
    {
        if (!(string.IsNullOrEmpty(user.FirstName) && string.IsNullOrEmpty(user.LastName) && string.IsNullOrEmpty(user.NickName)))
        {
            Context.User.Add(user);

            Context.SaveChanges();
            return "success";
        }
        return "fail";
    }

    


    
}
