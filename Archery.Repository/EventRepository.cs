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
                    Context.Event.Add(new() { Name = newEvent.Name, Parcour = newEvent.Parcour, User = newEvent.User, IsRunning = true });

                    Context.SaveChanges();
                }
                return "Event gestartet";
            }
            catch (Exception ex)
            {
                return "Fail: " + ex.Message;
            }
        }

        public string EndEvent(Event eventToStop)
        {
            try
            {
                var stopEvent = Context.Event.SingleOrDefault(e => e.Id == eventToStop.Id);

                if (stopEvent == null)
                {
                    return "Fail: stopEvent ist Null";
                }
                stopEvent.IsRunning = false;

                Context.SaveChanges();

                return "Event beendet";
            }
            catch(Exception ex)
            {
               return "Fail: " + ex.Message;               
            }
        }
    }
}
