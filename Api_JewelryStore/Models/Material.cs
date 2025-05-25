using System;
using System.Collections.Generic;

namespace Api_JewelryStore.Models;

public partial class Material
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<JewelryItem> JewelryItems { get; set; } = new List<JewelryItem>();
}
