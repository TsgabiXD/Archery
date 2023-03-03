using Microsoft.EntityFrameworkCore;

using Archery.Model;

namespace Archery.Repository;

public class ParcourRepository : AbstractRepository
{
    public ParcourRepository(ArcheryContext context) : base(context) {}

    public IEnumerable<Parcour> GetAllParcours() => Context.Parcour.AsNoTracking().ToList();

}
