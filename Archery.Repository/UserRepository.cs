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
            .Where(m => m.Event.IsRunning && m.User != null && !m.User.Hist);

        List<int> userIds = new();

        foreach (var aum in activeUserMappings)
            userIds.Add(aum.User!.Id);

        var inactiveUser = Context.User
            .Where(u => !userIds.Contains(u.Id) && !u.Hist)
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

        if (string.IsNullOrEmpty(user.FirstName) &&
            string.IsNullOrEmpty(user.LastName) &&
            string.IsNullOrEmpty(user.NickName))
            throw new Exception("Ungültige Werte!");

        if (Context.User.SingleOrDefault(u =>
                    u.FirstName == user.FirstName &&
                    u.LastName == user.LastName &&
                    u.NickName == user.NickName) != null)
            throw new Exception("no user found");

        // TODO möglicher Exploit
        var existingUser = Context.User.SingleOrDefault(u =>
            u.FirstName == user.FirstName &&
            u.LastName == user.LastName &&
            u.NickName == user.NickName);

        if (existingUser is null)
        {
            Context.User.Add(user);
            Context.SaveChanges();
            return "Benutzer hinzugefügt!";
        }
        else // TODO remove after all admins has been registered
            return "Benutzer existiert bereits!";
    }

    public string DeleteUser(int uid)
    {
        if (uid <= 0)
            throw new InvalidOperationException("Invalid UserID");

        var setUserNull = Context.User.SingleOrDefault(u => u.Id == uid);

        if (setUserNull is null)
            throw new Exception("Fail: User ist Null");

        setUserNull.Hist = true;

        Context.SaveChanges();
        return "succedfully deletet";
    }


}
