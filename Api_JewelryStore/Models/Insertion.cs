﻿using System;
using System.Collections.Generic;

namespace Api_JewelryStore.Models;

public partial class Insertion
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<InsertionsDetail> InsertionsDetails { get; set; } = new List<InsertionsDetail>();
}
