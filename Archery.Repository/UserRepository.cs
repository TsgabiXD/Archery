﻿using Microsoft.EntityFrameworkCore;
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
        if (user.FirstName.Length <= 150 && user.LastName.Length <= 150 && user.NickName.Length <= 150)
        {

            if (!(string.IsNullOrEmpty(user.FirstName) && string.IsNullOrEmpty(user.LastName) && string.IsNullOrEmpty(user.NickName)))
            {
                try
                {
                    Context.User.Add(user);
                    Context.SaveChanges();
                    return "Benutzer hinzugefügt";
                }
                catch (Exception ex)
                {
                    return "Fail: " + ex.Message;
                }
            }
            return "Ungültige Werte!";
        }
        return "Parcourname zu lang!";
    }
}
