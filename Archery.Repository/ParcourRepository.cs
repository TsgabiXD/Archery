using Microsoft.EntityFrameworkCore;

using Archery.Model;
using Microsoft.Extensions.Logging;

namespace Archery.Repository;

public class ParcourRepository : AbstractRepository
{
    public ParcourRepository(ArcheryContext context) : base(context) { }

    public IEnumerable<Parcour> GetAllParcours()
    {
        return Context.Parcour.AsNoTracking().ToList();
    }

    public Parcour GetParcour(int id)
    {
        try
        {
            return Context.Parcour.AsNoTracking().SingleOrDefault(p => p.Id == id);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("Fehler beim Suchden der ID", ex);
        }
    }

    public string AddParcour(Parcour parcour)
    {
        try
        {
            if (!(parcour.Name.Length <= 150))
                return "Vorname, Nachname oder Nickname zu lang!";

            if (string.IsNullOrEmpty(parcour.Name) || string.IsNullOrEmpty(parcour.Location) || parcour.AnimalNumber <= 0)
                return "Ungültige Werte!";

            Context.Parcour.Add(new()
            {
                AnimalNumber = parcour.AnimalNumber,
                Name = parcour.Name,
                Location = parcour.Location,
            });
            Context.SaveChanges();

            return "success";
        }
        catch (Exception ex)
        {
            return "Fail: " + ex.Message;
        }
    }
}

