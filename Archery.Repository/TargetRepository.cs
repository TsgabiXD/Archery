﻿using Archery.Model;
using Archery.Model.ApiHelper;
using Microsoft.EntityFrameworkCore;

namespace Archery.Repository
{
    public class TargetRepository : AbstractRepository
    {
        public TargetRepository(ArcheryContext context) : base(context) { }

        public IEnumerable<Target> GetMyTargets(int userId)
        {
            var mapping = Context.Mapping
                .Include(m => m.Event)
                .Include(m => m.User)
                .Include(m => m.Target)
                .Where(m => m.Event.IsRunning)
                .AsNoTracking()
                .ToList();
                // TODO Implementieren @TsgabiXD
            var targetFound = Context.Target.AsNoTracking().ToList();

            if (targetFound == null)
                throw new InvalidOperationException("Fehler beim Suchen der Ziele");

            return targetFound;
        }

        public string AddTarget(NewTarget newTarget)
        {
            if (!((newTarget.ArrowCount < 0 && newTarget.ArrowCount > 3) || (newTarget.HitArea < 1 && newTarget.HitArea > 3)))
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

                // TODO richtiges error handling @JoRole
                if (newTarget.ArrowCount == null || newTarget.HitArea == null)
                    // TODO assagekraeftige Errormeldung @JoRole
                    throw new InvalidOperationException("Fehler beim Hinzufügen des Ziels");

                Context.SaveChanges();
                return "Ziel hinzugefügt";

            }
            return "Ungültige Werte!";
        }
    }
}
