using Inventario.Core.Entities;

namespace Inventario.Core.Entities;

public class Brand : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
}