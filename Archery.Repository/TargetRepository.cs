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
            try
            {
                return Context.Target.AsNoTracking().ToList();
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Fehler beim Suchden der Ziele", ex);
            }
        }


        public string AddTarget(NewTarget newTarget)
        {
            if (!((newTarget.ArrowCount < 0 && newTarget.ArrowCount > 3) || (newTarget.HitArea < 1 && newTarget.HitArea > 3)))
            {
                try
                {
                    var eventfilter = Context.Mapping.FirstOrDefault(x => x.Event.Id == newTarget.EventId && x.User.Id == newTarget.UserId);
                    var currentTarget = Context.Target.Add(new() { ArrowCount = newTarget.ArrowCount, HitArea = newTarget.HitArea, });
                    eventfilter.Target.Add(currentTarget.Entity);

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
