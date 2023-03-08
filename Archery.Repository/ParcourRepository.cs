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

    public void AddParcour(string name, string location, int animalNumber)
    {
        if(!(string.IsNullOrEmpty(name)) && string.IsNullOrEmpty(location) && animalNumber <= 0)
        {
            Context.Parcour.Add(new() { Name = name, Location = location, AnimalNumber = animalNumber });

            Context.SaveChanges();
        }
    }
}
