using System;
using System.Collections.Generic;

namespace TallerEntity.Models;

public partial class Reservation
{
    public Guid ReservationID { get; set; }

    public Guid CustomerID { get; set; }

    public int RoomID { get; set; }

    public DateTime ReservationDate { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public int Customersln { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
