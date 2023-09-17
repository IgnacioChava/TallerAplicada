using System;
using System.Collections.Generic;

namespace TallerEntity.Models;

public partial class Customer
{
    public int CustomerID { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
