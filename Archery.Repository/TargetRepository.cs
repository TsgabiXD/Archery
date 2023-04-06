using Archery.Model;
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
                .Where(m => m.Event.IsRunning && m.User != null && m.User.Id == userId)
                .AsNoTracking()
                .ToList();

            List<Target> targetFound = new();

            foreach (var m in mapping)
                foreach (var t in m.Target)
                    targetFound.Add(t);

            if (targetFound == null)
                throw new Exception("Kein Ziel gefunden!");

            return targetFound;
        }

        public string AddTarget(NewTarget newTarget, int userId)
        {
            if (newTarget.ArrowCount > 0 && newTarget.ArrowCount < 4 &&
                newTarget.HitArea > 0 && newTarget.HitArea < 4 &&
                newTarget.EventId > 0)
            {
                var eventfilter = Context.Mapping
                    .Include(m => m.Event)
                    .Include(m => m.User)
                    .Include(m => m.Target)
                    .FirstOrDefault(m => m.Event.Id == newTarget.EventId &&
                                    m.User != null &&
                                    m.User.Id == userId);

                if (eventfilter is null)
                    throw new Exception("Ein Mappingfehler ist passiert!");

                eventfilter.Target.Add(new() { ArrowCount = newTarget.ArrowCount, HitArea = newTarget.HitArea, });

                Context.SaveChanges();
                return "Ziel hinzugefügt";

            }
            throw new ArgumentException("Ungültige Werte!");
        }
    }
}
