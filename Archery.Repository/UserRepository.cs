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

    public IEnumerable<int> GetUsersRunningEvents(int id)
    {
        var mappings = Context.Mapping
                                .Include(m => m.User)
                                .Include(m => m.Event)
                                .Where(m => m.User != null && m.User.Id == id)
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
        if (user != null)
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
        throw new InvalidOperationException("Fehler beim Hinzufügen des Users");
    }
}
