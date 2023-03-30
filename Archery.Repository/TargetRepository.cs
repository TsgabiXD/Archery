﻿using Archery.Model;
using Archery.Model.ApiHelper;
using Microsoft.EntityFrameworkCore;

namespace Archery.Repository
{
    public class TargetRepository : AbstractRepository
    {
        public TargetRepository(ArcheryContext context) : base(context) { }

        public IEnumerable<Target> GetAllTargets()
        {
            var targetFound = Context.Target.AsNoTracking().ToList();

            if (targetFound == null)
                throw new InvalidOperationException("Fehler beim Suchen des Ziels");

            return targetFound;
        }

        public string AddTarget(NewTarget newTarget)
        {
            if (!((newTarget.ArrowCount < 0 && newTarget.ArrowCount > 3) || (newTarget.HitArea < 1 && newTarget.HitArea > 3)))
            {
                try
                {
                    var eventfilter = Context.Mapping
                                                .Include(m => m.Event)
                                                .Include(m => m.User)
                                                .Include(m => m.Target)
                                                .FirstOrDefault(m => m.Event.Id == newTarget.EventId &&
                                                                        m.User != null &&
                                                                        m.User.Id == newTarget.UserId);

                    if (eventfilter is null)
                        throw new Exception();

                    eventfilter.Target.Add(new() { ArrowCount = newTarget.ArrowCount, HitArea = newTarget.HitArea, });

                    Context.SaveChanges();
                    return "Ziel hinzugefügt";
                }
                catch (Exception ex)
                {
                    return "Fail: " + ex.Message;
                }
            }
            return "Ungültige Werte!";
        }
    }
}
