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


        public IEnumerable<AdminViewElement> GetAdminViewElements()
        {
            var userEventMappings = Context.Mapping
                                    .Include(m => m.User)
                                    .Include(m => m.Event)
                                    .Where(m => m.Event.IsRunning)
                                    .AsNoTracking()
                                    .ToArray();

            var mappings = Context.Mapping
                                        .Include(m => m.Target)
                                        .Include(m => m.Event)
                                        .Include(m => m.User)
                                        .Where(m => m.Event.IsRunning)
                                        .AsNoTracking()
                                        .ToArray();

            if (userEventMappings is null)
                throw new Exception();

            List<AdminViewElement> result = new();

            foreach (var uem in userEventMappings)
            {
                result.Add(new() { EventName = uem.Event.Name });

                var countingResults = new int[3, 3]{
                                                    {20, 18, 16},
                                                    {14, 12, 10},
                                                    {8, 6, 4},
                                                };

                foreach (var m in mappings.Where(m => m.Event.Id == uem.Event.Id && m.User?.Id == uem.User?.Id))
                {
                    var score = 0;
                    var targetsOfUser = m.Target;

                    foreach (var target in targetsOfUser)
                        score += countingResults[target.ArrowCount, target.HitArea];

                    result.Last().User.Add(new() { NickName = uem.User.NickName, Score = score });
                }
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
