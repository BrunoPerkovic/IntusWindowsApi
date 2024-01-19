using System.ComponentModel.DataAnnotations;

namespace IntusWindowsAPI.BL.Data;

public abstract class DbBase
{
    [Key] public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}