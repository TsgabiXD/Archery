using System.ComponentModel.DataAnnotations.Schema;

namespace Archery.Model;

public class Parcour
{
    public int Id { set; get; }

    [Column(TypeName = "nvarchar(150)")]
    public string Name { set; get; } = null!;

    public int AnimalNumber { set; get; }

    public string Location { set; get; } = null!;
}
