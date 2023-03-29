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

    public bool CheckUser(string nickName)
    {
        return Context.User.ToList().FirstOrDefault(u => u.NickName == nickName) is not null;
    }

    public IEnumerable<Mapping> GetUserWithTargets(int eventId)
    {
        var mapping = Context.Mapping
                                .Include(m => m.Event)
                                .Include(m => m.Target)
                                .Include(m => m.User)
                                .SingleOrDefault(m => m.Event != null && m.Event.Id == eventId);

        if (mapping is null)
            throw new Exception();

        return mapping;
    }

    public IEnumerable<int> GetUsersRunningEvents(int id)
    {
        var mappings = Context.Mapping.Where(m => m.User != null && m.User.Id == id)
                                      .AsNoTracking()
                                      .ToList();
        List<int> result = new();

        foreach (var mapping in mappings)
            if (!result.Contains(mapping.Event.Id) && mapping.Event.IsRunning == true)
                result.Add(mapping.Event.Id);

        return result;
    }

    public string AddUser(User user)
    {
        try
        {
            if (user.FirstName.Length <= 150 && user.LastName.Length <= 150 && user.NickName.Length <= 150)
            {
                // TODO möglicher Exploit
                if ((!(string.IsNullOrEmpty(user.FirstName) &&
                        string.IsNullOrEmpty(user.LastName) &&
                        string.IsNullOrEmpty(user.NickName))) &&
                        Context.User.FirstOrDefault(u => u.FirstName == user.FirstName
                                                        && u.LastName == user.LastName
                                                        && u.NickName == user.NickName) is null)
                {
                    Context.User.Add(user);
                    Context.SaveChanges();
                    return "Benutzer hinzugefügt";
                }
                return "Ungültige Werte!";
            }
            return "Parcourname zu lang!";
        }
        catch (Exception ex)
        {
            return "Fail: " + ex.Message;
        }
    }
}
