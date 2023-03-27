using Microsoft.EntityFrameworkCore;

using Archery.Model;
using Microsoft.Extensions.Logging;

namespace Archery.Repository;

public class ParcourRepository : AbstractRepository
{
    public ParcourRepository(ArcheryContext context) : base(context) { }

    public IEnumerable<Parcour> GetAllParcours()
    {
        var parcourFound = Context.Parcour.AsNoTracking().ToList();

        if (parcourFound == null)
            throw new InvalidOperationException("Fehler beim Suchen des Parcours");

        return parcourFound;
    }

    public Parcour GetParcour(int id)
    {
        var parcourFound = Context.Parcour.AsNoTracking().SingleOrDefault(p => p.Id == id);

        if (parcourFound == null)
            throw new InvalidOperationException("Fehler beim Suchden der ID");

        return parcourFound;
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

