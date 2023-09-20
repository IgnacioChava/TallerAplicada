using System;
using System.Collections.Generic;

namespace TallerEntity.Models;

public partial class Room
{
    public int RoomID { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public int? Capacity { get; set; }

    public decimal? Price { get; set; }

    public bool? AvailabilityRoom { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
