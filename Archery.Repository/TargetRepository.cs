using Archery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Archery.Model;

namespace Archery.Repository
{
    public class TargetRepository : AbstractRepository
    {
        public TargetRepository(ArcheryContext context) : base(context){}
        public IEnumerable<Target> GetAllTargets()
        {
            return Context.Target.AsNoTracking().ToList();
        }

        public void AddTarget(int arrowCount, int hitArea)
        {
            if (!((arrowCount < 0 && arrowCount > 3) || (hitArea < 1 && hitArea > 3)))
            {
                Context.Target.Add(new() { ArrowCount = arrowCount, HitArea = hitArea });

                Context.SaveChanges();
            }
        }
    }
}
