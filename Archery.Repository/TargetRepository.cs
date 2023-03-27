using Archery.Model;
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


        public string AddTarget(int arrowCount, int hitArea)
        {
            if (!((arrowCount < 0 && arrowCount > 3) || (hitArea < 1 && hitArea > 3)))
            {
                try
                {
                    Context.Target.Add(new() { ArrowCount = arrowCount, HitArea = hitArea });
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
