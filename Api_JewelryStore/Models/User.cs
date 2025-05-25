﻿using System;
using System.Collections.Generic;

namespace Api_JewelryStore.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public decimal? Money { get; set; }

    public bool? IsAdmin { get; set; }

    public virtual ICollection<CardItem> CardItems { get; set; } = new List<CardItem>();
}
