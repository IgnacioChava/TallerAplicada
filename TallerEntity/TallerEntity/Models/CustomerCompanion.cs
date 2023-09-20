using System;
using System.Collections.Generic;

namespace TallerEntity.Models;

public partial class CustomerCompanion
{
    public int CustomerCompanionID { get; set; }

    public int CustomerID { get; set; }

    public int RoomsID { get; set; }
}
