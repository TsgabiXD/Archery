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

            if (mapping is null)
                throw new Exception("Ein Mappingfehler ist passiert!");

            List<Target> targetFound = new();
            var eventId = mapping.First().Event.Id;

            foreach (var m in mapping)
                if (m.Event.Id == eventId)
                    foreach (var t in m.Target)
                        targetFound.Add(t);

            var animalCount = Context.Event
                .Include(e => e.Parcour)
                .Single(e => e.Id == eventId)
                .Parcour.AnimalNumber;

            if (targetFound == null)
                throw new Exception("Kein Ziel gefunden!");

            var results = new Target[animalCount];

            if (targetFound.Count > animalCount)
                throw new Exception($"Too much Targets for one Event! \nTargets found: {targetFound.Count} \nMax Animals:{animalCount}");

            for (var i = 0; i < targetFound.Count; i++)
                results[i] = targetFound.ElementAt(i);

            return results;
        }

        public string AddTarget(NewTarget newTarget, int userId)
        {
            if (!(newTarget.ArrowCount > 0 && newTarget.ArrowCount < 4 &&
                newTarget.HitArea > 0 && newTarget.HitArea < 4 &&
                newTarget.EventId > 0))
                throw new ArgumentException("Ungültige Werte!");

            if (!Context.Event
                .Include(e => e.Parcour)
                .Where(e => e.Id == newTarget.EventId)
                .Any())
                throw new Exception("Event wurde bereits beendet!");

            var eventfilter = Context.Mapping
                .Include(m => m.Event)
                .Include(m => m.User)
                .Include(m => m.Target)
                .FirstOrDefault(m => m.Event.Id == newTarget.EventId &&
                                m.User != null &&
                                m.User.Id == userId);

            if (eventfilter is null)
                throw new Exception("Ein Mappingfehler ist passiert!");

            var targetsAlreadyHit = GetMyTargets(userId)
                .Where(t => t is not null)
                .Count();
            var targetLimit = Context.Event
                .Include(e => e.Parcour)
                .Where(e => e.Id == newTarget.EventId)
                .First()
                .Parcour
                .AnimalNumber;

            Console.WriteLine();
            Console.WriteLine($"\nTargets found: {targetsAlreadyHit} \nMax Animals:{targetLimit}");
            Console.WriteLine();

            if (targetsAlreadyHit >= targetLimit)
                throw new Exception("Es wurden bereits alle Ziele notiert!");

            eventfilter.Target.Add(new() { ArrowCount = newTarget.ArrowCount, HitArea = newTarget.HitArea });

            Context.SaveChanges();
            return "Ziel hinzugefügt";
        }
    }
}
