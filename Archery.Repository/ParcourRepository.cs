using Microsoft.EntityFrameworkCore;

using Archery.Model;

namespace Archery.Repository;

public class ParcourRepository : AbstractRepository
{
    public ParcourRepository(ArcheryContext context) : base(context) {}

    public IEnumerable<Parcour> GetAllParcours()
    {
        return Context.Parcour.AsNoTracking().ToList();
    }

    public Parcour GetParcour(int id)
    {
        return Context.Parcour.AsNoTracking().Single(p => p.Id == id);
    }

    public string AddParcour(Parcour parcour)
    {
        if(!(string.IsNullOrEmpty(parcour.Name) && string.IsNullOrEmpty(parcour.Location) && parcour.AnimalNumber <= 0))
        {
            Context.Parcour.Add(parcour);
            Context.SaveChanges();

            return "success";
        }

        return "fail";
    }
}
