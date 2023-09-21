using System;
using System.Collections.Generic;

namespace TallerEntity.Models;

public partial class CustomerCompanion
{
    public Guid CustomerID { get; set; }

    public int Cedula { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public Guid? CompanionID { get; set; }

    public virtual Customer? Companion { get; set; }
}
