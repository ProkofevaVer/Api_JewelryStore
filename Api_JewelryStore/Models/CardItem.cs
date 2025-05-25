using System;
using System.Collections.Generic;

namespace Api_JewelryStore.Models;

public partial class CardItem
{
    public int Id { get; set; }

    public int? JewelrySizesItemId { get; set; }

    public int? UserId { get; set; }

    public string? Status { get; set; }

    public int? CardQuantity { get; set; }

    public decimal? CardTotalPrice { get; set; }

    public DateOnly? CardDate { get; set; }

    public virtual JewelrySize? JewelrySizesItem { get; set; }

    public virtual User? User { get; set; }
}
