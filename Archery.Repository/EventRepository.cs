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
            try
            {
                if (newEvent != null)
                {

                    var eventUser = Context.User
                        .Where(u => newEvent.UserIds.Contains(u.Id))
                        .ToList();

                    var eventParcour = Context.Parcour
                        .SingleOrDefault(u => newEvent.ParcourId == u.Id);

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
