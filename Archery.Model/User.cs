using System.ComponentModel.DataAnnotations.Schema;

namespace Archery.Model;

public class User
{
    public int Id { set; get; }

    [Column(TypeName = "nvarchar(150)")]
    public string FirstName { set; get; } = null!;
    

    [Column(TypeName = "nvarchar(150)")]
    public string LastName { set; get; } = null!;
    

    [Column(TypeName = "nvarchar(150)")]
    public string NickName { set; get; } = null!;

    // TODO Add Authorisation
    public string Role { set; get; } = "User";
}
