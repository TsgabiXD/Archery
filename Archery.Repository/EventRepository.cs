using Archery.Model;
using Archery.Model.ApiHelper;
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

        public string StartEvent(NewEvent newEvent)
        {
            if (newEvent != null)
            {

                var eventUser = Context.User
                    .Where(u => newEvent.UserIds.Contains(u.Id))
                    .ToList();

                var eventParcour = Context.Parcour
                    .SingleOrDefault(u => newEvent.ParcourId == u.Id);

                if (eventParcour is null)
                    throw new Exception();

                var e = Context.Event.Add(new() { Name = newEvent.Name, Parcour = eventParcour, IsRunning = true }).Entity;

                foreach (var user in eventUser)
                    Context.Mapping.Add(new() { Event = e, User = user });

                if (e == null)
                {
                    return "Event not found";
                }

                Context.SaveChanges();

                return e.Id.ToString();
            }
            throw new InvalidOperationException("Fehler beim Starten des Events");
        }
    }

    public IEnumerable<AdminViewElement> GetAdminViewElements()
    {
        var userEventMapping = Context.Mapping
                                .Include(m => m.User)
                                .Include(m => m.Event)
                                .Where(m => m.Event.IsRunning)
                                .AsNoTracking()
                                .ToArray();

        var targetEventMapping = Context.Mapping
                                    .Include(m => m.Target)
                                    .Include(m => m.Event)
                                    .Where(m => m.Event.IsRunning)
                                    .AsNoTracking()
                                    .ToArray();

        if (userEventMapping is null)
            throw new Exception();

        List<AdminViewElement> result = new();

        foreach (var uem in userEventMapping)
        {
            result.Add(new() { EventName = uem.Event.Name });

            int score = 0;
            int[,] countingResults = new int[3, 3]{
                                                    {20, 18, 16},
                                                    {14, 12, 10},
                                                    {8, 6, 4},
                                                };
            // TODO implementieren
            // foreach (var targetEvent in targetEventMapping.Where(m => m.Event.Id == uem.Event.Id))
            //     targetEvent.Target

            result.Last().User.Add(new() { NickName = uem.User.NickName, Score = score });
        }

        return result;
    }

    public string EndEvent(int eventToStop)
    {
        if (eventToStop != null)
        {
            var stopEvent = Context.Event.SingleOrDefault(e => e.Id == eventToStop);

            if (stopEvent == null)
            {
                return "Fail: stopEvent ist Null";
            }


            stopEvent.IsRunning = false;

            Context.SaveChanges();

            return " Event beendet";
        }
        throw new InvalidOperationException("Fehler beim Beenden des Events");
    }
}
    }
}
