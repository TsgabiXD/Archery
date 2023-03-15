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
            if (newEvent != null)
            {
                Context.Event.Add(new() {Name = newEvent.Name, Parcour = newEvent.Parcour, User = newEvent.User, IsRunning = true });

                Context.SaveChanges();

                return "Event gestartet";
            }
            return "fail";
            
        }

        public string EndEvent(Event eventToStop)
        {

            var stopEvent = Context.Event.AsNoTracking().Single(e => e.Id == eventToStop.Id);

            stopEvent.IsRunning = false;

            Context.SaveChanges();

            return "Event beendet";
        }
    }
}
