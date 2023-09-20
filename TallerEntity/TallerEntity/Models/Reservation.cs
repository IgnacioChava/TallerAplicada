using System;
using System.Collections.Generic;

namespace TallerEntity.Models;

public partial class Reservation
{
    public int ReservationID { get; set; }

    public int? CustomerID { get; set; }

    public int? RoomID { get; set; }

    public DateTime? ReservationDate { get; set; }

    public DateTime? CheckInDate { get; set; }

    public DateTime? CheckOutDate { get; set; }

    public int? CustomersIn { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Room? Room { get; set; }
}
