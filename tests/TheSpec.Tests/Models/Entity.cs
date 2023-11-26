using System.ComponentModel.DataAnnotations;

namespace TheSpecTests.Models;

public abstract class Entity
{
    [Key]
    public int Id { get; set; }
}
