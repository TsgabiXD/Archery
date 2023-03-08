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

        public void AddTarget(int arrowNumber, int hittedArea)
        {
            if (!((arrowNumber < 0 && arrowNumber > 3) || (hittedArea < 1 && hittedArea > 3)))
            {
                Context.Target.Add(new() { ArrowNumber = arrowNumber, HittedArea = hittedArea });

                Context.SaveChanges();
            }
        }
    }
}
