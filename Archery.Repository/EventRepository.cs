using Archery.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archery.Repository
{
    public class EventRepository : AbstractRepository
    {
        public EventRepository(ArcheryContext context) : base(context)
        {
        }

        public string StartEvent(Event newEvent)
        {
            try
            {
                if (newEvent != null)
                {
                    var eventParcour = Context.Parcour.Single(p => p.Id == newEvent.Parcour.Id);

                    List<int> ids = new();

                    foreach (var user in newEvent.User)
                        foreach (var dbUser in Context.User)
                            if (dbUser.Id == user.Id)
                                ids.Add(dbUser.Id);

                    var eventUser = Context.User
                        .Where(u => ids.Contains(u.Id))
                        .ToList();

                    var e = Context.Event.Add(new() { Name = newEvent.Name, Parcour = eventParcour, User = eventUser, IsRunning = true }).Entity;

                    if (e == null)
                    {
                        return "Event not found";
                    }

                    Context.SaveChanges();

                    return e.Id.ToString();
                }
                throw new Exception();
            }
            catch (Exception ex)
            {
                return "Fail: " + ex.Message;
            }
        }

        public string EndEvent(int eventToStop)
        {
            try
            {
                var stopEvent = Context.Event.SingleOrDefault(e => e.Id == eventToStop);

                if (stopEvent == null)
                {
                    return "Fail: stopEvent ist Null";
                }

                List<int> eventIds = new();

                foreach(var user in stopEvent.User)
                    foreach(var dbUser in Context.User)
                        if(dbUser.Id == user.Id) eventIds.Add(dbUser.Id);

                var filterUser = Context.User
                    .Where(t => eventIds.Contains(t.Id))
                    .ToList();


                stopEvent.IsRunning = false;

                Context.SaveChanges();

                return " Event beendet";
            }
            catch (Exception ex)
            {
                return "Fail: " + ex.Message;
            }
        }
    }
}
