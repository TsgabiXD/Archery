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

                    var id = Context.Event.Single(e => 
                            e.Name == newEvent.Name && 
                            e.Parcour == newEvent.Parcour &&
                            e.User == newEvent.User &&
                            e.IsRunning == true )
                        .Id;

                    Context.SaveChanges();

                    return id.ToString();
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

                return "Event beendet";
            }
            catch (Exception ex)
            {
                return "Fail: " + ex.Message;
            }
        }
    }
}
