using System;
using System.Collections.Generic;

namespace TallerEntity.Models;

public partial class CustomerReservationsView
{
    public Guid CustomerID { get; set; }

    public string NameCustomer { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? CodeRoom { get; set; }
}
