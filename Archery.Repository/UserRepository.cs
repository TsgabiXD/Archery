using Microsoft.EntityFrameworkCore;
using Archery.Model;

namespace Archery.Repository;

public class UserRepository : AbstractRepository
{
    public UserRepository(ArcheryContext context) : base(context) { }

    public IEnumerable<User> GetAllInactiveUsers()
    {
        var activeUserMappings = Context.Mapping
            .Include(m => m.User)
            .Include(m => m.Event)
            .AsNoTracking()
            .Where(m => m.Event.IsRunning && m.User != null);

        List<int> userIds = new();

        foreach (var ium in activeUserMappings)
            userIds.Add(ium.User!.Id);

        var inactiveUser = Context.User
            .Where(u => !userIds.Contains(u.Id))
            .AsNoTracking()
            .ToList();

        return inactiveUser;
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
        if (user == null)
            throw new InvalidOperationException("Fehler beim Hinzufügen des Users");

        if (!(user.FirstName.Length <= 150 && user.LastName.Length <= 150 && user.NickName.Length <= 150))
            throw new Exception("Parcourname zu lang!");

        // TODO möglicher Exploit
        if (!((!(string.IsNullOrEmpty(user.FirstName) &&
                string.IsNullOrEmpty(user.LastName) &&
                string.IsNullOrEmpty(user.NickName))) &&
                Context.User.FirstOrDefault(u => u.FirstName == user.FirstName
                                                && u.LastName == user.LastName
                                                && u.NickName == user.NickName) is null))
            throw new Exception("Ungültige Werte!");

        Context.User.Add(user);
        Context.SaveChanges();
        return "Benutzer hinzugefügt";
    }
}
