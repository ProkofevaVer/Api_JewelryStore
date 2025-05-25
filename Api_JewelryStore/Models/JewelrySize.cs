using System;
using System.Collections.Generic;

namespace Api_JewelryStore.Models;

public partial class JewelrySize
{
    public int Id { get; set; }

    public int? JewelryItemId { get; set; }

    public int? Size { get; set; }

    public int? StockQuantity { get; set; }

    public virtual ICollection<CardItem> CardItems { get; set; } = new List<CardItem>();

    public virtual JewelryItem? JewelryItem { get; set; }
}
