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

    public string AddUser(string firstname, string lastname, string nickname)
    {
        if (!(string.IsNullOrEmpty(firstname) && string.IsNullOrEmpty(lastname) && string.IsNullOrEmpty(nickname)))
        {
            Context.User.Add(new() { FirstName = firstname, LastName = lastname, NickName = nickname });

            Context.SaveChanges();
            return "success";
        }
        return "fail";
    }

    


    
}
