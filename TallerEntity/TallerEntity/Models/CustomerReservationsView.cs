using System;
using System.Collections.Generic;

namespace TallerEntity.Models;

public partial class CustomerReservationsView
{
    public int CustomerID { get; set; }

    public string? NameCustomer { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? CodeRoom { get; set; }
}
